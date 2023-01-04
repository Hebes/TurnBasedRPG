using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 物品拓展专用 Component文件夹内的物品组件获取专用拓展
/// </summary>
public static class PrefabExpand
{
    public class PrefabExpandConfig1
    {
        public Transform parent { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public GameObject prefab { get; set; }
        public UnityAction<BaseEventData, GameObject> PointerEnterCallBack { get; set; }
        public UnityAction<BaseEventData, GameObject> PointerExitCallBack { get; set; }
        public UnityAction<GameObject> unityAction1 { get; set; }
    }

    /// <summary>
    /// ItemPanel_Item 拓展专用
    /// </summary>
    public static void Prefa_ItemPanel_Item(this Transform transform, PrefabExpandConfig1 prefabExpandConfig)
    {
        Transform itemPanel_Item = transform.InstantiatePrefa(prefabExpandConfig.parent);
        transform.OnGetText("T_amount").text = $"x{prefabExpandConfig.amount}";
        GameObject T_select = itemPanel_Item.OnGetTransform("T_select").gameObject;
        itemPanel_Item.OnGetButton("T_name").onClick.AddListener(() =>
        {
            prefabExpandConfig.unityAction1?.Invoke(itemPanel_Item.gameObject);
        });
        Transform T_nameTF = itemPanel_Item.OnGetTransform("T_name");
        itemPanel_Item.OnGetText("T_name").text = prefabExpandConfig.name;
        T_select.SetActive(false);
        T_nameTF.AddEventTriggerListener(EventTriggerType.PointerEnter, (baseEventData) =>
        {
            prefabExpandConfig.PointerEnterCallBack?.Invoke(baseEventData, T_select);
        });
        T_nameTF.AddEventTriggerListener(EventTriggerType.PointerExit, (baseEventData) =>
        {
            prefabExpandConfig.PointerExitCallBack?.Invoke(baseEventData, T_select);
        });
    }

    /// <summary>
    /// CharacterInfo_optionItem 拓展专用
    /// </summary>
    public static void Prefa_CharacterInfo_optionItem(this Transform transform, PrefabExpandConfig1 prefabExpandConfig)
    {
        Transform CharacterInfo_optionItemTemp = transform.InstantiatePrefa(prefabExpandConfig.parent);
        GameObject T_select = CharacterInfo_optionItemTemp.OnGetTransform("T_select").gameObject;
        CharacterInfo_optionItemTemp.OnGetText("T_Info").text = prefabExpandConfig.name;
        CharacterInfo_optionItemTemp.OnGetButton("T_button").onClick.AddListener(() =>
        {
            prefabExpandConfig.unityAction1?.Invoke(T_select);
        });
        T_select.SetActive(false);
        CharacterInfo_optionItemTemp.AddEventTriggerListener(EventTriggerType.PointerEnter, (baseEventData) =>
        {
            prefabExpandConfig.PointerEnterCallBack?.Invoke(baseEventData, T_select);
        });
        CharacterInfo_optionItemTemp.AddEventTriggerListener(EventTriggerType.PointerExit, (baseEventData) =>
        {
            prefabExpandConfig.PointerExitCallBack?.Invoke(baseEventData, T_select);
        });
    }

    /// <summary>
    /// 就是TargetButton 拓展专用
    /// </summary>
    public static void Prefa_EnemySelectButton(this Transform enemyButton, PrefabExpandConfig1 prefabExpandConfig)
    {
        Transform newButton = enemyButton.InstantiatePrefa(prefabExpandConfig.parent);//创建敌人选择按钮
        newButton.OnGetText("T_Text").text = $"{prefabExpandConfig.name}";
        newButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            prefabExpandConfig.unityAction1?.Invoke(null);
        });
        newButton.AddEventTriggerListener(EventTriggerType.PointerEnter, (baseEventData) =>
        {
            prefabExpandConfig.PointerEnterCallBack?.Invoke(baseEventData, null);
        });
        newButton.AddEventTriggerListener(EventTriggerType.PointerExit, (baseEventData) =>
        {
            prefabExpandConfig.PointerExitCallBack?.Invoke(baseEventData, null);
        });
    }
}
