/******************************************
	作者：暗沉
	邮箱：空
	日期：2022-12-06 11:10:14
	功能：战斗界面管理器

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
/// 战斗管理类
/// </summary>
public class BattleManager : MonoBehaviour
{

    /// <summary>
    /// 执行操作
    /// </summary>
    public enum PerformAction
    {
        /// <summary>
        /// 等待
        /// </summary>
        WAIT,
        /// <summary>
        /// 采取行动
        /// </summary>
        TAKEACTION,
        /// <summary>
        /// 执行动作
        /// </summary>
        PERFROMACTION,
        /// <summary>
        /// 检查敌人或者玩家是否存活
        /// </summary>
        CHECKALIVE,
        /// <summary>
        /// 赢
        /// </summary>
        WIN,
        /// <summary>
        /// 输
        /// </summary>
        LOSE

    }


    private void Awake()
    {
       
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}


