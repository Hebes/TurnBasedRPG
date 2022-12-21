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

/// <summary>
/// 设置界面
/// </summary>
public class SettingPanel : BasePanel
{
    private SettingPanelComponent panelComponent { get; set; }

    public override void AwakePanel(object obj)
    {
        base.AwakePanel(obj);
		panelComponent = new SettingPanelComponent(this, gameObject);
		panelComponent.Init();
	}

	/// <summary>
	/// close按钮
	/// </summary>
    internal void V_closeAddListener()
    {
		GameRoot.Instance.uiModule.HidePanel(ConfigUIPrefab.SettingPanel);
	}

	/// <summary>
	/// 关闭面板要做的事情
	/// </summary>
    public override void HidePanel(object obj)
    {
        base.HidePanel(obj);
		DLog.Log("关闭了SettingPanel面板");
    }
}


