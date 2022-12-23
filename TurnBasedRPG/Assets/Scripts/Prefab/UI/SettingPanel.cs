/******************************************
	作者：暗沉
	邮箱：空
	日期：2022-12-04 20:32:19
	功能：设置界面

	//===============================\
				空
	\===============================//
******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using LogUtils;
using System;
using DG.Tweening;

/// <summary>
/// 设置界面
/// </summary>
public class SettingPanel : BasePanel
{
    #region UI代码 自动化生成
    #region 组件模块
    public Button T_closeButton { set; get; }

    public Image T_closeImage { set; get; }

    public Transform T_closeTransform { set; get; }

    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_closeButton = transform.OnGetButton("T_close");

        T_closeImage = transform.OnGetImage("T_close");

        T_closeTransform = transform.OnGetTransform("T_close");

    }
    #endregion

    #region 按钮监听模块
    /// <summary>
    /// 按钮监听
    /// </summary>
    private void OnAddListener()
    {
        T_closeButton.onClick.AddListener(T_closeAddListener);
    }
    #endregion

    #endregion

    #region 自动化获取代码组件
    public Image SettingPanelImage { set; get; }
    public Transform SettingPanelTransform { set; get; }
    public CanvasGroup SettingPanelCanvasGroup { set; get; }

    /// <summary>
    /// 获取选中物体的组件
    /// </summary>
    private void OnGetSelectComponent()
    {
        SettingPanelImage = GetComponent<Image>();
        SettingPanelTransform = GetComponent<Transform>();
        SettingPanelCanvasGroup = GetComponent<CanvasGroup>();
    }
    #endregion


    public override void AwakePanel(object obj)
    {
        base.AwakePanel(obj);
        OnGetSelectComponent();
        OnGetComponent();
        OnAddListener();
    }

    public override void ShowPanel(object obj = null)
    {
        base.ShowPanel(obj);
        PanelFadeIn();
    }

    /// <summary>
    /// close按钮
    /// </summary>
    internal void T_closeAddListener()
    {
        GameRoot.Instance.uiModule.HidePanel(ConfigUIPrefab.SettingPanel);
    }

    /// <summary>
    /// 关闭面板要做的事情
    /// </summary>
    public override void HidePanel(object obj)
    {
        base.HidePanel(obj);
        PanelFadeOut();
        DLog.Log("关闭了SettingPanel面板");
    }

    //**********************************************动画效果**********************************************
    /// <summary>
    /// 进面板时
    /// </summary>
    private void PanelFadeIn()
    {
        SettingPanelCanvasGroup.alpha = 0f;
        (transform as RectTransform).localPosition = new Vector3(0f, -1000f, 0f);
        (transform as RectTransform).DOAnchorPos(Vector2.zero, 1f, false).SetEase(Ease.OutCirc);
        SettingPanelCanvasGroup.DOFade(1f, 1f);
    }

    /// <summary>
    /// 出面板时
    /// </summary>
    private void PanelFadeOut()
    {
        SettingPanelCanvasGroup.alpha = 1f;
        (transform as RectTransform).localPosition = Vector3.zero;
        (transform as RectTransform).DOAnchorPos(new Vector2(0f, -1000f), 1f, false).SetEase(Ease.InOutQuint);
        SettingPanelCanvasGroup.DOFade(0f, 1f);
    }
}


