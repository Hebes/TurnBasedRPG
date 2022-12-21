using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : BasePanel
{
	#region UI代码 自动化生成
	#region 组件模块
	public Image T_LoadProgressBarImage { set; get; }

	public Transform T_LoadProgressBarTransform { set; get; }
	public Transform T_TitleTransform { set; get; }
	public Transform T_LoadProgressTransform { set; get; }
	public Transform T_MessageTransform { set; get; }

	public Text T_TitleText { set; get; }
	public Text T_LoadProgressText { set; get; }
	public Text T_MessageText { set; get; }

	#endregion

	#region 获取组件模块
	/// <summary>
	/// 获取组件
	/// </summary>
	private void OnGetComponent()
	{
		T_LoadProgressBarImage = transform.OnGetImage("T_LoadProgressBar");

		T_LoadProgressBarTransform = transform.OnGetTransform("T_LoadProgressBar");
		T_TitleTransform = transform.OnGetTransform("T_Title");
		T_LoadProgressTransform = transform.OnGetTransform("T_LoadProgress");
		T_MessageTransform = transform.OnGetTransform("T_Message");

		T_TitleText = transform.OnGetText("T_Title");
		T_LoadProgressText = transform.OnGetText("T_LoadProgress");
		T_MessageText = transform.OnGetText("T_Message");

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




	public override void AwakePanel(object obj = null)
    {
        base.AwakePanel(obj);
		OnGetComponent();
	}

	/// <summary>
	/// 更新进度条
	/// </summary>
	public void UpdataProgress(float progress,string message)
    {
		T_LoadProgressBarTransform.localScale = new Vector2(progress, T_LoadProgressBarTransform.localScale.y);
		T_LoadProgressText.text = $"{(int)(progress * 100)}%";
		T_MessageText.text = message;
	}
}
