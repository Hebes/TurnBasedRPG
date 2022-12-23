using LogUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 敌人选择按钮
/// </summary>
public class EnemySelectButton : MonoBehaviour
{
    #region UI代码
    #region 组件模块
    public Text T_TextText { set; get; }
    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_TextText = transform.OnGetText("T_Text");
    }
    #endregion

    #endregion

    public GameObject EnemyPrefab;//敌人物体

    public void Init()
    {
        OnGetComponent();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SelectEnemy);
        button.onClick.AddListener(HideSelector);

        //EventTrigger监听
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        //进入
        UnityAction<BaseEventData> Enter = new UnityAction<BaseEventData>((BaseEventData) => { ShowSelector(); });
        EventTrigger.Entry myEnter = new EventTrigger.Entry();
        myEnter.eventID = EventTriggerType.PointerEnter;
        myEnter.callback.AddListener(Enter);
        eventTrigger.triggers.Add(myEnter);
        //退出
        UnityAction<BaseEventData> Exit = new UnityAction<BaseEventData>((BaseEventData) => { HideSelector(); });
        EventTrigger.Entry myExit = new EventTrigger.Entry();
        myExit.eventID = EventTriggerType.PointerExit;
        myExit.callback.AddListener(Exit);
        eventTrigger.triggers.Add(myExit);
    }

    /// <summary>
    /// 选择敌人 拽托到自己的按钮上
    /// </summary>
    public void SelectEnemy()
    {
        //TODO 选择敌人 拽托到自己的按钮上
        GameRoot.Instance.battleManager.Input2(EnemyPrefab);
    }

    /// <summary>
    /// 关闭 - 鼠标滑动头上黄色物体
    /// </summary>
    public void HideSelector()
    {
        //Debug.Log("进入EventTrigger监听代码");
        EnemyPrefab.transform.Find("T_Selector").gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示 -鼠标滑动头上黄色物体
    /// </summary>
    public void ShowSelector()
    {
        //Debug.Log("进入EventTrigger监听代码");
        EnemyPrefab.transform.Find("T_Selector").gameObject.SetActive(true);
    }
}
