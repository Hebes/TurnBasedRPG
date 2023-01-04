using LogUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanel : BasePanel
{
    #region UI代码 自动化生成
    #region 组件模块
    public Button T_ItemPanel_closeButton { set; get; }

    public Image T_ItemPanel_closeImage { set; get; }
    public Image T_ItemPanelImage { set; get; }
    public Image T_MagicPanelImage { set; get; }
    public Image T_selectImage { set; get; }

    public Transform T_ItemPanel_closeTransform { set; get; }
    public Transform T_ItemPanelTransform { set; get; }
    public Transform T_MagicPanelTransform { set; get; }
    public Transform T_selectTransform { set; get; }
    public Transform T_nameTransform { set; get; }
    public Transform T_ItemPanelContentTransform { set; get; }
    public Transform T_itemFiltrateContentTransform { set; get; }
    public Transform T_MagicPanellContentTransform { set; get; }

    public Text T_nameText { set; get; }

    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_ItemPanel_closeButton = transform.OnGetButton("T_ItemPanel_close");

        T_ItemPanel_closeImage = transform.OnGetImage("T_ItemPanel_close");
        T_ItemPanelImage = transform.OnGetImage("T_ItemPanel");
        T_MagicPanelImage = transform.OnGetImage("T_MagicPanel");
        T_selectImage = transform.OnGetImage("T_select");

        T_ItemPanel_closeTransform = transform.OnGetTransform("T_ItemPanel_close");
        T_ItemPanelTransform = transform.OnGetTransform("T_ItemPanel");
        T_MagicPanelTransform = transform.OnGetTransform("T_MagicPanel");
        T_selectTransform = transform.OnGetTransform("T_select");
        T_nameTransform = transform.OnGetTransform("T_name");
        T_ItemPanelContentTransform = transform.OnGetTransform("T_ItemPanelContent");
        T_itemFiltrateContentTransform = transform.OnGetTransform("T_itemFiltrateContent");
        T_MagicPanellContentTransform = transform.OnGetTransform("T_MagicPanellContent");

        T_nameText = transform.OnGetText("T_name");

    }
    #endregion

    #region 按钮监听模块
    /// <summary>
    /// 按钮监听
    /// </summary>
    private void OnAddListener()
    {
        T_ItemPanel_closeButton.onClick.AddListener(T_ItemPanel_closeAddListener);
    }
    #endregion

    #endregion





    List<Transform> panels = new List<Transform>();
    public Transform ItemPanel_Item { get; private set; }
    public Transform CharacterInfo_optionItem { get; private set; }
    //装备筛选
    public List<string> itemFiltrateLists { get; set; } = new List<string>()
    {
        "Consumables",
        "BothHandsEquipment",
        "SingleHandsEquipment",
        "BodyEquipment",
        "LeftHandsEquipment",
        "RightHandsEquipment",
    };

    public override void AwakePanel(object obj = null)
    {
        base.AwakePanel(obj);
        OnGetComponent();
        OnAddListener();
        LoadPrefab();

        panels.Add(T_ItemPanelTransform);
        panels.Add(T_MagicPanelTransform);
    }
    public override void ShowPanel(object obj = null)
    {
        base.ShowPanel(obj);
        ShowChildPanel((obj as UIInfo<ItemPanel>).Data.ToString());
    }

    /// <summary>
    /// 加载物体
    /// </summary>
    private void LoadPrefab()
    {
        ItemPanel_Item = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.ItemPanel_Item);
        CharacterInfo_optionItem = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.CharacterInfo_optionItem);
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    private void ShowChildPanel(string str)
    {
        ChangePanel(str);
        switch (str)
        {
            case "T_ItemPanel":
                T_itemFiltrateContentTransform.ClearChild();
                List<ItemInfo> ItemInfoLists = GameRoot.Instance.dataMgr.ItemInfoLists;
                ShowItemPanel(ItemInfoLists, (itemInfo) => { return true; });
                //筛选列表
                itemFiltrateLists?.ForEach((str) =>
                {
                    string tempStr = string.Empty;
                    switch (str)
                    {
                        case "Consumables": tempStr = "消耗品"; break;
                        case "BothHandsEquipment": tempStr = "双手装备"; break;
                        case "SingleHandsEquipment": tempStr = "单手装备"; break;
                        case "BodyEquipment": tempStr = "身体装备"; break;
                        case "LeftHandsEquipment": tempStr = "左手装备"; break;
                        case "RightHandsEquipment": tempStr = "右手装备"; break;
                    }
                    CharacterInfo_optionItem.Prefa_CharacterInfo_optionItem(new PrefabExpand.PrefabExpandConfig1()
                    {
                        name = tempStr,
                        parent = T_itemFiltrateContentTransform,
                        unityAction1 = (go) => { ShowItemPanel(ItemInfoLists, (itemInfo) => { return itemInfo.Type.Equals(str); }); },
                    });
                });
                break;
            case "T_MagicPanel":
                break;
        }
    }

    /// <summary>
    /// 显示物品面板
    /// </summary>
    private void ShowItemPanel(List<ItemInfo> ItemInfoLists, Func<ItemInfo, bool> action)
    {
        T_ItemPanelContentTransform.ClearChild();
        //物品列表
        ItemInfoLists?.ForEach((itemInfo) =>
        {
            if ((bool)(action?.Invoke(itemInfo)))//可以实例化的条件
            {
                ItemPanel_Item.Prefa_ItemPanel_Item(new PrefabExpand.PrefabExpandConfig1()
                {
                    name = itemInfo.Name,
                    parent = T_ItemPanelContentTransform,
                    PointerEnterCallBack = (baseEventDat, go) => { go.SetActive(true); },
                    PointerExitCallBack = (baseEventDat, go) => { go.SetActive(false); },
                    amount = itemInfo.Amount,
                    unityAction1 = (go) => { Debug.Log(itemInfo.Name); },
                });
            }
        });
    }



    /// <summary>
    /// 关闭按钮
    /// </summary>
    public void T_ItemPanel_closeAddListener()
    {
        DLog.Log("ItemPanel关闭");
        PanelExpand.HidePanel(this.name);
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <param name="transform"></param>
    private void ChangePanel(string str)
    {
        panels?.ForEach((panel) =>
        {
            panel.gameObject.SetActive(panel.name.Equals(str));
        });
    }
}
