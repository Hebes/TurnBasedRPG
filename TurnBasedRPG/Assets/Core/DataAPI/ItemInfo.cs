

using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : CSVBaseInfo
{
	#region 自动化生成文件

	/// <summary>序号</summary>
	public int ID { get; set; }

	/// <summary>名称</summary>
	public string Name { get; set; }

	/// <summary>描述</summary>
	public string Description { get; set; }

	/// <summary>类型</summary>
	public string Type { get; set; }

	/// <summary>是否可堆叠</summary>
	public bool IsStack { get; set; }

	/// <summary>数量</summary>
	public int Amount { get; set; }

	/// <summary>生命</summary>
	public int HP { get; set; }

	/// <summary>防御</summary>
	public float Def { get; set; }

	/// <summary>攻击力</summary>
	public float Atk { get; set; }

    public override int GetID()
    {
		return ID;
    }

    #endregion
}
