using LogUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : BasePanel
{
    #region UI代码 自动化生成
    #region 组件模块
    public Transform T_ContentTransform { set; get; }

    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_ContentTransform = transform.OnGetTransform("T_Content");

    }
    #endregion

    #region 按钮监听模块
    /// <summary>
    /// 按钮监听
    /// </summary>
    private void OnAddListener()
    {

    }
    #endregion

    #endregion

    private List<string> btnLists { get; } = new List<string>()
    {
        "道具",
        "魔法",
        "职业",
        "装备",
        "角色",
        "设定",
        "存档",
    };
    public Transform SelectPanel_Button { get; private set; }

    public override void AwakePanel(object obj = null)
    {
        base.AwakePanel(obj);
        OnGetComponent();
        OnAddListener();
        LoadPrefab();
        ShowButton();
    }

    /// <summary>
    /// 加载物体
    /// </summary>
    private void LoadPrefab()
    {
        SelectPanel_Button = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.SelectPanel_Button);
    }

    /// <summary>
    /// 显示按钮
    /// </summary>
    private void ShowButton()
    {
        btnLists?.ForEach((str) =>
        {
            Transform selectPanel_Button = SelectPanel_Button.InstantiatePrefa(T_ContentTransform);
            selectPanel_Button.OnGetButton().onClick.AddListener(() => { ButtonListener(str); });
            selectPanel_Button.OnGetText("T_Text").text = $"{str}";
        });
    }

    private void ButtonListener(string str)
    {
        switch (str)
        {
            case "道具":
                DLog.Log("道具");
                GameRoot.Instance.uiModule.ShowPanel<ItemPanel>(new UIInfo<ItemPanel>()
                {
                    panelName = ConfigUIPrefab.ItemPanel,
                    layer = E_UI_Layer.Bottom,
                    Data = "T_ItemPanel"
                });
                break;
            case "魔法":
                DLog.Log("魔法");
                GameRoot.Instance.uiModule.ShowPanel<ItemPanel>(new UIInfo<ItemPanel>()
                {
                    panelName = ConfigUIPrefab.ItemPanel,
                    layer = E_UI_Layer.Bottom,
                    Data = "T_MagicPanel"
                });
                break;
            case "职业":
                DLog.Log("职业");
                break;
            case "装备":
                DLog.Log("装备");
                break;
            case "角色":
                DLog.Log("角色");
                GameRoot.Instance.uiModule.ShowPanel<CharacterInfoPanel>(new UIInfo<CharacterInfoPanel>()
                {
                    panelName = ConfigUIPrefab.CharacterInfoPanel,
                    layer = E_UI_Layer.Bottom,
                });
                break;
            case "设定":
                DLog.Log("设定");
                GameRoot.Instance.uiModule.ShowPanel<SettingPanel>(new UIInfo<SettingPanel>()
                {
                    panelName = ConfigUIPrefab.SettingPanel,
                    layer = E_UI_Layer.System,
                });
                break;
            case "存档":
                DLog.Log("存档");
                break;
        }
    }
}
