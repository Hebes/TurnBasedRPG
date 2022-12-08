using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBar : MonoBehaviour
{
	#region UI����
	#region ���ģ��
	public Text T_NameText { set; get; }
	public Text T_HPMPText { set; get; }

	#endregion

	#region ��ȡ���ģ��
	/// <summary>
	/// ��ȡ���
	/// </summary>
	private void OnGetComponent()
	{
		T_NameText = transform.OnGetText("T_Name");
		T_HPMPText = transform.OnGetText("T_HPMP");
	}
	#endregion
    #endregion

    private void Awake()
    {
		OnGetComponent();
	}

	/// <summary>
	/// ������Ϣ
	/// </summary>
	/// <param name="name"></param>
	/// <param name="HP"></param>
	/// <param name="MP"></param>
	public void OnSetInfo(string name, string HP, string MP)
	{
		T_NameText.text = name;
		OnSetInfo(HP, MP);
	}

	/// <summary>
	/// ������Ϣ
	/// </summary>
	/// <param name="HP"></param>
	/// <param name="MP"></param>
	public void OnSetInfo(string HP, string MP)
	{
		T_HPMPText.text = $"HP:{HP}\nMP:{MP}";
	}
}
