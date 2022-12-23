using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌人管理
/// </summary>
public class EnemyStateMaschine : MonoBehaviour
{
    /// <summary>战斗管理类</summary>
    public BattleManager BM { get; private set; }

    /// <summary>敌人属性</summary>
    public BaseEnemy enemy;

    /// <summary>回合状态</summary>
    public enum TurnState
    {
        /// <summary>
        /// 进度条上升
        /// </summary>
        PROCESSING,
        /// <summary>
        /// 选择敌人行动  
        /// </summary>
        CHOOSEACTION,
        /// <summary>
        /// 等待
        /// </summary>
        WAITING,
        /// <summary>
        /// 行动
        /// </summary>
        ACTION,
        /// <summary>
        /// 死去的
        /// </summary>
        DEAD,
    }

    [Tooltip("当前冷却时间")] public float cur_colldown = 0f;
    [Tooltip("一共冷却时间")] public float max_colldown = 5f;

    /// <summary>选择器物体 就是角色头上顶的黄色小物体</summary>
    [Tooltip("选择器物体 就是角色头上顶的黄色小物体")] public GameObject Selector;

    /// <summary>
    /// 这个物体的初始位置
    /// </summary>
    public Vector3 startPosition { get; set; }

    /// <summary>
    /// 状态机
    /// </summary>
    public FSMSystem enemyFSMSystem { get; private set; }

    /// <summary>
    /// 是时候行动了
    /// </summary>
    //private bool actionStarted = false;

    /// <summary>
    /// 要攻击的英雄
    /// </summary>
    [Tooltip("要攻击的英雄")] public GameObject HeroToAttAck;

    /// <summary>
    /// 获取组件
    /// </summary>
    private void Awake()
    {
        Selector = transform.OnGetTransform("T_Selector").gameObject;//选择器
        Selector.SetActive(false);//设置选择器关闭
        startPosition = transform.position;//设置初始位置
        BM = GameRoot.Instance.battleManager;

        enemyFSMSystem = new FSMSystem();
        enemyFSMSystem.stateDic = new Dictionary<string, FSMState>()
        {
            {TurnState.PROCESSING.ToString(),new EnemyProcessingState(enemyFSMSystem,this) },
            {TurnState.CHOOSEACTION.ToString(),new EnemyChooseactionState(enemyFSMSystem,this) },
            {TurnState.WAITING.ToString(),new EnemyWaitingState(enemyFSMSystem,this) },
            {TurnState.ACTION.ToString(),new EnemyActionState(enemyFSMSystem,this) },
            {TurnState.DEAD.ToString(),new EnemyDeadState(enemyFSMSystem,this) },
        };
        enemyFSMSystem.ChangeGameState(TurnState.PROCESSING.ToString(), this);
        Debug.Log("怪物的当前状态是：" + enemyFSMSystem.GetCurState);
    }
    private void Update()
    {
        enemyFSMSystem.Update();
    }

    public Coroutine MonoModuleStartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    /// <summary>
    /// 遭受伤害
    /// </summary>
    public void TakeDamage(float getDamageAmount)
    {
        enemy.curHP -= getDamageAmount;
        if (enemy.curHP <= 0)
        {
            enemy.curHP = 0;
            enemyFSMSystem.ChangeGameState(TurnState.DEAD.ToString());
        }
    }
}
