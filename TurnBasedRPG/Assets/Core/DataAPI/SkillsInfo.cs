

using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : CSVBaseInfo
{
	#region 自动化生成文件

	/// <summary>序号</summary>
	public int ID { get; set; }

	/// <summary>名称</summary>
	public string AttackName { get; set; }

	/// <summary>攻击描述</summary>
	public string AttackDescription { get; set; }

	/// <summary>伤害</summary>
	public float AttackDamage { get; set; }

	/// <summary>法力值消耗</summary>
	public float AttackCost { get; set; }

	/// <summary>技能类型</summary>
	public int SkillType { get; set; }
	#endregion

	public override int GetID()
	{
		return ID;
	}
}
