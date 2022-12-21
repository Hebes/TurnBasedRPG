

using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo: CSVBaseInfo
{
	#region 自动化生成文件

	/// <summary>序号</summary>
	public int ID { get; set; }

	/// <summary>名称</summary>
	public string Name { get; set; }

	/// <summary>基础血量</summary>
	public float BaseHP { get; set; }

	/// <summary>基础魔法值</summary>
	public float BaseMP { get; set; }

	/// <summary>基础攻击力</summary>
	public float BaseATK { get; set; }

	/// <summary>基础防御力</summary>
	public float BaseDef { get; set; }

	/// <summary>技能攻击方式</summary>
	public List<int> Attacks { get; set; }

	/// <summary>魔法攻击方式</summary>
	public List<int> MagicAttacks { get; set; }

	public override int GetID()
	{
		return ID;
	}
    #endregion
}
