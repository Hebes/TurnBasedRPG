/******************************************
	作者：暗沉
	邮箱：空
	日期：2022-12-04 20:36:40
	功能：

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

/// <summary>
/// 
/// </summary>
public class SettingPanelComponent : BaseUIGetComponents_new
{
    public SettingPanel settingPanel { get; private set; }
    public SettingPanelComponent(SettingPanel settingPanel, GameObject Obj) : base(Obj)
    {
		this.settingPanel = settingPanel;
	}

	public Button V_closeButton { set; get; }

	public Image V_closeImage { set; get; }

	public Transform V_closeTransform { set; get; }

    public override void Init()
    {
        base.Init();
		OnGetComponent();
		OnAddListener();
	}

    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
	{
		V_closeButton = GetButton("V_close");

		V_closeImage = GetImage("V_close");

		V_closeTransform = GetTransform("V_close");
	}

	/// <summary>
	/// 按钮监听
	/// </summary>
	private void OnAddListener()
	{
		V_closeButton.onClick.AddListener(settingPanel.V_closeAddListener);
	}
}


