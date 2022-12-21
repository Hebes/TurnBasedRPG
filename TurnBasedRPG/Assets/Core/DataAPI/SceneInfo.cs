

using System.Collections.Generic;
using UnityEngine;

public class SceneInfo : CSVBaseInfo
{
	#region 自动化生成文件

	/// <summary>序号</summary>
	public int ID { get; set; }

	/// <summary>场景</summary>
	public string Scene { get; set; }

	/// <summary>怪物列表</summary>
	public List<int> Enemy { get; set; }

    public override int GetID()
    {
		return ID;
    }

    #endregion
}
