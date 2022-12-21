using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogUtils;
using UnityEngine.UI;

public class BattlePanel : BasePanel
{

    #region UI代码
    #region 组件模块
    public Transform T_SelectTargetPanelTransform { set; get; }
    public Transform T_ActionPanelTransform { set; get; }
    public Transform T_MagicPanelTransform { set; get; }
    public Transform T_HeroPanelSpacerTransform { set; get; }
    public Transform T_TargetSpacerTransform { set; get; }
    public Transform T_ActionSpacerTransform { set; get; }
    public Transform T_MagicSpacerTransform { set; get; }

    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_SelectTargetPanelTransform = transform.OnGetTransform("T_SelectTargetPanel");
        T_ActionPanelTransform = transform.OnGetTransform("T_ActionPanel");
        T_MagicPanelTransform = transform.OnGetTransform("T_MagicPanel");
        T_HeroPanelSpacerTransform = transform.OnGetTransform("T_HeroPanelSpacer");
        T_TargetSpacerTransform = transform.OnGetTransform("T_TargetSpacer");
        T_ActionSpacerTransform = transform.OnGetTransform("T_ActionSpacer");
        T_MagicSpacerTransform = transform.OnGetTransform("T_MagicSpacer");
    }
    #endregion
    #endregion

    public override void AwakePanel(object obj)
    {
        base.AwakePanel(obj);
        OnGetComponent();
    }
    public override void ShowPanel(object obj)
    {
        base.ShowPanel(obj);
    }

    public override void HidePanel(object obj)
    {
        base.HidePanel(obj);
        DLog.Log("关闭了BattlePanel面板");
    }

    /// <summary>
    /// 选择敌人面板
    /// </summary>
    /// <param name="isOpen"></param>
    public void SelectTargetPanel(bool isOpen)
    {
        T_SelectTargetPanelTransform.gameObject.SetActive(isOpen);
    }

    /// <summary>
    /// 选择攻击方式面板
    /// </summary>
    /// <param name="isOpen"></param>
    public void ActionPanel(bool isOpen)
    {
        T_ActionPanelTransform.gameObject.SetActive(isOpen);
    }

    /// <summary>
    /// 选择魔法面板
    /// </summary>
    /// <param name="isOpen"></param>
    public void MagicPanel(bool isOpen)
    {
        T_MagicPanelTransform.gameObject.SetActive(isOpen);
    }

    /// <summary>
    /// 清理英雄的冷却条
    /// </summary>
    public void ClearHeroBar()
    {
        T_HeroPanelSpacerTransform.tfClearChild();
    }
}
