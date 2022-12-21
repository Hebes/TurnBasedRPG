using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI层级
/// </summary>
public enum E_UI_Layer
{
    /// <summary>
    /// 底部的
    /// </summary>
    Bottom,
    /// <summary>
    /// 中间的
    /// </summary>
    Mid,
    /// <summary>
    /// 顶部的
    /// </summary>
    Top,
    /// <summary>
    /// 系统级别
    /// </summary>
    System,
}

/// <summary>
/// 打开UI的配置类
/// </summary>
/// <typeparam name="T"></typeparam>
public class UIInfo<T>
{
    /// <summary>
    /// 需要显示的面板名称
    /// </summary>
    public string panelName { get; set; }
    /// <summary>
    /// 面板所在的层
    /// </summary>
    public E_UI_Layer layer { get; set; } = E_UI_Layer.Bottom;
    /// <summary>
    /// 回调事件
    /// </summary>
    public UnityAction<T> callBack { get; set; } = null;

    /// <summary>
    /// 自己传入的数据类型 可以传任意
    /// </summary>
    public object Data { get; set; }
}

/// <summary>
/// UI管理模块
/// </summary>
public class UIModule : BaseManager<UIModule>
{
    public Dictionary<string, BasePanel> panelDic { get; }
    public Transform bottom { get; private set; }
    public Transform mid { get; private set; }
    public Transform top { get; private set; }
    public Transform system { get; private set; }
    public RectTransform canvas { get; private set; }

    public UIModule()
    {
        panelDic = new Dictionary<string, BasePanel>();

        //创建Canvas 让其过场景的时候 不被移除
        GameObject obj = GameObject.Find("Canvas");
        canvas = obj.transform as RectTransform;

        //找到各层
        bottom = canvas.Find("Bottom");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");
    }

    /// <summary>
    /// 显示面板,重新打开也是这个
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uIInfo"></param>
    public void ShowPanel<T>(UIInfo<T> uIInfo) where T : BasePanel
    {
        if (panelDic.ContainsKey(uIInfo.panelName))
        {
            panelDic[uIInfo.panelName].ShowPanel(uIInfo);
            // 处理面板创建完成后的逻辑
            uIInfo.callBack?.Invoke(panelDic[uIInfo.panelName] as T);
            //避免面板重复加载 如果存在该面板 即直接显示 调用回调函数后  直接return 不再处理后面的异步加载逻辑
            return;
        }

        ResModule.GetInstance().LoadAsync<GameObject>("Prefabs/UI/" + uIInfo.panelName, (obj) =>
        {

            //把他作为 Canvas的子对象
            //并且 要设置它的相对位置
            //找到父对象 你到底显示在哪一层
            Transform father = bottom;
            switch (uIInfo.layer)
            {
                case E_UI_Layer.Mid: father = mid; break;
                case E_UI_Layer.Top: father = top; break;
                case E_UI_Layer.System: father = system; break;
            }
            //设置父对象  设置相对位置和大小
            obj.name = uIInfo.panelName;
            obj.transform.SetParent(father);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMin = (obj.transform as RectTransform).offsetMax = Vector2.zero;
            //得到预设体身上的面板脚本
            T panel = obj.GetComponent<T>();
            if (panel == null)
                panel = obj.AddComponent<T>();
            // 处理面板创建完成后的逻辑
            panel.AwakePanel(uIInfo);
            uIInfo.callBack?.Invoke(panel);
            panel.ShowPanel(uIInfo);
            //把面板存起来
            panelDic.Add(uIInfo.panelName, panel);
        });
    }

    /// <summary>
    /// 通过层级枚举 得到对应层级的父对象
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.Bottom: return this.bottom;
            case E_UI_Layer.Mid: return this.mid;
            case E_UI_Layer.Top: return this.top;
            case E_UI_Layer.System: return this.system;
            default: return null;
        }
    }

    /// <summary>
    /// 移除面板
    /// </summary>
    /// <param name="panelName"></param>
    public void RemovePanel(string panelName, object obj = null)
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HidePanel(obj);
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="panelName"></param>
    public void HidePanel(string panelName, object obj = null)
    {
        if (panelDic.ContainsKey(panelName))
        {
            if (panelDic[panelName].isActive)
            {
                panelDic[panelName].HidePanel(obj);
                panelDic[panelName].gameObject.SetActive(panelDic[panelName].isActive);
            }
        }
    }

    /// <summary>
    /// 得到某一个已经显示的面板 方便外部使用
    /// </summary>
    public T GetPanel<T>(string name) where T : BasePanel
    {
        if (panelDic.ContainsKey(name))
            return panelDic[name] as T;
        return null;
    }

    /// <summary>
    /// 给控件添加自定义事件监听
    /// </summary>
    /// <param name="control">控件对象</param>
    /// <param name="type">事件类型</param>
    /// <param name="callBack">事件的响应函数</param>
    public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    }
}
