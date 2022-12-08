using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMaschine : MonoBehaviour
{
    /// <summary>
    /// 战斗管理类
    /// </summary>
    public BattleManager BM { get; private set; }

    public BaseHero hero;

    /// <summary>
    /// 回合状态
    /// </summary>
    public enum TurnState
    {
        /// <summary>
        /// 进度条上升
        /// </summary>
        PROCESSING,
        /// <summary>
        /// 添加到列表中
        /// </summary>
        ADDTOLIST,
        /// <summary>
        /// 等待
        /// </summary>
        WAITING,
        /// <summary>
        /// 选择
        /// </summary>
        SELECTING,
        /// <summary>
        /// 行动
        /// </summary>
        ACTION,
        /// <summary>
        /// 死去的
        /// </summary>
        DEAD,
    }

    public TurnState currentState;

    /// <summary>
    /// 当前冷却时间
    /// </summary>
    private float cur_colldown { get; set; } = 0f;

    /// <summary>
    /// 最长冷却时间
    /// </summary>
    private float max_colldown { get; set; } = 5f;

    /// <summary>
    /// 冷却版进度条
    /// </summary>
    public Image ProgressBar;

    /// <summary>
    /// 选择器物体 就是角色头上顶的黄色小物体
    /// </summary>
    public GameObject Selector;

    //英雄回跳
    public GameObject EnemyToAttack;
    private bool actionStarted = false;
    public Vector3 startPosition;
    private float animSpeed = 10f;

    /// <summary>
    /// dead 是否存活
    /// </summary>
    private bool alive = true;

    //进度条 heroPanel
    private HeroBar stats;
    /// <summary>
    /// 玩家的行动冷却条 HeroBar
    /// </summary>
    public GameObject HeroPanel;
    private Transform HeroPanelSpacer;
}
