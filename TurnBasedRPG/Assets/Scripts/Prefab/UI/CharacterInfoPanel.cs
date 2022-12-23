using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 玩家信息管理界面
/// </summary>
public class CharacterInfoPanel : BasePanel
{

    #region UI代码 自动化生成
    #region 组件模块
    public Text T_timeText { set; get; }
    public Text T_goldText { set; get; }

    public Transform T_timeTransform { set; get; }
    public Transform T_goldTransform { set; get; }
    public Transform T_btnContentTransform { set; get; }
    public Transform T_playerContentTransform { set; get; }

    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_timeText = transform.OnGetText("T_time");
        T_goldText = transform.OnGetText("T_gold");

        T_timeTransform = transform.OnGetTransform("T_time");
        T_goldTransform = transform.OnGetTransform("T_gold");
        T_btnContentTransform = transform.OnGetTransform("T_btnContent");
        T_playerContentTransform = transform.OnGetTransform("T_playerContent");
        
    }
    #endregion

    #region 按钮监听模块
    /// <summary>
    /// 按钮监听
    /// </summary>
    private void OnAddListener()
    {
        CreatOptionItem();
    }
    #endregion

    #endregion

    public Transform CharacterInfo_optionItem { get; private set; }
    public Transform CharacterInfo_heroInfo { get; private set; }
    private List<string> btnLists { get; } = new List<string>()
    {
        "道具",
        "魔法",
        "职业",
        "装备",
        "状态",
        "设定",
        "存档",
		//"技能",
		"退出界面",
    };

    public override void AwakePanel(object obj = null)
    {
        base.AwakePanel(obj);
        LoadPrefab();
        OnGetComponent();
        OnAddListener();
    }
    public override void ShowPanel(object obj = null)
    {
        base.ShowPanel(obj);
        CreatHeroBar();
    }

    /// <summary>
    /// 加载需要的预制体
    /// </summary>
    private void LoadPrefab()
    {
        CharacterInfo_optionItem = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.CharacterInfo_optionItem);
        CharacterInfo_heroInfo = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.CharacterInfo_heroInfo);
    }

    private void GetData()
    {
        
    }

    /// <summary>
    /// 生成左边选择按钮
    /// </summary>
    private void CreatOptionItem()
    {
        btnLists.ForEach((str) =>
        {
            Transform CharacterInfo_optionItemTemp = Instantiate(CharacterInfo_optionItem, T_btnContentTransform);
            #region 直接获取组件模块
            GameObject T_select = CharacterInfo_optionItemTemp.OnGetTransform("T_select").gameObject;
            CharacterInfo_optionItemTemp.OnGetButton("T_button").onClick.AddListener(() => { BtnAddListener(str); T_select.gameObject.SetActive(false); });
            CharacterInfo_optionItemTemp.OnGetText("T_Info").text = str;
            T_select.gameObject.SetActive(false);
            UIModule.AddCustomEventListener(CharacterInfo_optionItemTemp, EventTriggerType.PointerEnter,(baseEventData) => 
            {
                T_select.gameObject.SetActive(true);
            });
            UIModule.AddCustomEventListener(CharacterInfo_optionItemTemp, EventTriggerType.PointerExit, (baseEventData) =>
            {
                T_select.gameObject.SetActive(false);
            });
            #endregion
        });
    }

    /// <summary>
    /// 创建英雄信息条
    /// </summary>
    private void CreatHeroBar()
    {
        T_playerContentTransform.ClearChild();
        List<GameObject> heroBattleLists = GameRoot.Instance.sceneManager.heroBattleLists;
        for (int i = 0; i < heroBattleLists?.Count; i++)
        {
            HeroStateMaschine heroStateMaschine = heroBattleLists[i].GetComponent<HeroStateMaschine>();
               Transform characterInfo_heroInfo = Instantiate(CharacterInfo_heroInfo, T_playerContentTransform);
            characterInfo_heroInfo.GetComponentInChildren<Button>("GameObject/T_icon");
            characterInfo_heroInfo.GetComponentInChildren<Image>("GameObject/T_icon");
            characterInfo_heroInfo.GetComponentInChildren<Transform>("GameObject/T_icon");
            characterInfo_heroInfo.GetComponentInChildren<Image>("GameObject/T_select");
            characterInfo_heroInfo.GetComponentInChildren<Transform>("GameObject/T_select").gameObject.SetActive(false);
            characterInfo_heroInfo.GetComponentInChildren<Text>("GameObject/T_name").text= heroStateMaschine.hero.theName;
            characterInfo_heroInfo.GetComponentInChildren<Text>("GameObject/T_HPMP").text = $"{heroStateMaschine.hero.curHP}/{heroStateMaschine.hero.baseHP}\n{heroStateMaschine.hero.curMP}/{heroStateMaschine.hero.BaseMP}";
            characterInfo_heroInfo.GetComponentInChildren<Text>("GameObject/T_info").text = $"LV:{heroStateMaschine.hero.LV}\n职业:{"测试"}";

        }

        //GetComponentInChildren
    }
    /// <summary>
    /// 按钮添加监听
    /// </summary>
    /// <param name="str"></param>
    private void BtnAddListener(string str)
    {
        
        switch (str)
        {
            case "道具":
                break;
            case "魔法":
                break;
            case "职业":
                break;
            case "装备":
            case "状态":
                break;
            case "设定":
                GameRoot.Instance.uiModule.ShowPanel<SettingPanel>(new UIInfo<SettingPanel>()
                {
                    panelName = ConfigUIPrefab.SettingPanel,
                    layer = E_UI_Layer.System,
                });
                break;
            case "存档":
                break;
            case "退出界面":
                GameRoot.Instance.uiModule.HidePanel(this.name);
                break;
        }

    }
}
