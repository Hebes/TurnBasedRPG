using LogUtils;
using System.Collections;
using UnityEngine;

/// <summary>
/// 怪物攻击
/// </summary>
internal class EnemyActionState : FSMState
{
    public EnemyStateMaschine enemyStateMaschine { get; private set; }

    public EnemyActionState(FSMSystem fSMSystem, EnemyStateMaschine enemyStateMaschine) : base(fSMSystem)
    {
        this.enemyStateMaschine = enemyStateMaschine;
    }

    /// <summary>是时候行动了</summary>
    private bool actionStarted { get; set; } = false;
    /// <summary>移动的速度</summary>
    private float animSpeed { get; set; } = 30f;
    /// <summary>战斗中怪物和玩家的间隔</summary>
    private float interval = 1.5f;

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        enemyStateMaschine.MonoModuleStartCoroutine(TimeForAction());
    }

    // <summary>
    /// 行动的时间到了
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeForAction()
    {
        //Debug.Log("初始位置：" + enemyStateMaschine.startPosition);
        if (actionStarted) yield break;//如果没到可以行动,直接跳出协程
        actionStarted = true;
        //播放敌人接近英雄的攻击动画
        Vector3 heroPostion = new Vector3(
            enemyStateMaschine.HeroToAttAck.transform.position.x - interval,
            enemyStateMaschine.HeroToAttAck.transform.position.y,
            enemyStateMaschine.HeroToAttAck.transform.position.z);
        while (MoveTowrdsEnemy(heroPostion))//循环等待1帧
            yield return null;//这个是等待1帧的意思
        //等待
        yield return new WaitForSeconds(0.5f);
        //伤害
        DoDamage();
        //回到起始位置的动画
        Vector3 firstPosition = enemyStateMaschine.startPosition;
        while (MoveTowrdsStart(firstPosition))//循环等待1帧
            yield return null;//这个是等待1帧的意思
        //从BSM的Performer列表移除--执行行动列表移除
        enemyStateMaschine.BM.PerformList.RemoveAt(0);
        //重置BSM->等待
        enemyStateMaschine.BM.BattleManagerFSMSystem.ChangeGameState(BattleManager.PerformAction.WAIT.ToString());
        //结束协程
        actionStarted = false;
        //重置敌人状态
        enemyStateMaschine.enemy.cur_colldown = 0f;
        //进入冷却条上升状态
        enemyStateMaschine.enemyFSMSystem.ChangeGameState(EnemyStateMaschine.TurnState.PROCESSING.ToString());
    }

    /// <summary>
    /// 移动敌人 如果敌人没移动到玩家坐标的时候  返回的就是false
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool MoveTowrdsEnemy(Vector3 target)
    {
        //Debug.Log(enemyStateMaschine.gameObject.name + "需要移动到的坐标点：" + target + "当前坐标点：" + enemyStateMaschine.transform.position);
        return target != (enemyStateMaschine.transform.position =
            Vector3.MoveTowards(enemyStateMaschine.transform.position, target, animSpeed * Time.deltaTime));
    }

    /// <summary>
    /// 回到原来的位置
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool MoveTowrdsStart(Vector3 target)
    {
        //Debug.Log(enemyStateMaschine.gameObject.name + "需要移动到的坐标点：" + target + "当前坐标点：" + enemyStateMaschine.transform.position);
        return target != (enemyStateMaschine.transform.position =
            Vector3.MoveTowards(enemyStateMaschine.transform.position, target, animSpeed * Time.deltaTime));
    }

    /// <summary>
    /// 给与伤害
    /// </summary>
    private void DoDamage()
    {
        //TODO 技能伤害没有加上
        float calc_damage = enemyStateMaschine.enemy.curAtk;// + enemyStateMaschine.BM.PerformList[0].choosenAttack.attackDamage;
        enemyStateMaschine.HeroToAttAck.GetComponent<HeroStateMaschine>().TakeDamage(calc_damage);
    }
}