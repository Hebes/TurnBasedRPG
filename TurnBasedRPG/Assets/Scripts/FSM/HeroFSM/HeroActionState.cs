using LogUtils;
using System.Collections;
using UnityEngine;

/// <summary>
/// 英雄行动状态
/// </summary>
internal class HeroActionState : FSMState
{
    public HeroStateMaschine heroStateMaschine { get; private set; }

    public HeroActionState(FSMSystem fSMSystem, HeroStateMaschine heroStateMaschine) : base(fSMSystem)
    {
        this.heroStateMaschine = heroStateMaschine;
    }

    private bool actionStarted { get; set; } = false;
    private float animSpeed { get; set; } = 10f;

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        heroStateMaschine.MonoModuleStartCoroutine(TimeForAction());
    }

    //public override void DOUpdata()
    //{
    //    base.DOUpdata();
    //    if (heroStateMaschine.BM.BattleManagerFSMSystem.GetCurState==BattleManager.PerformAction.WIN.ToString())
    //    {
    //        GameRoot.Instance.monoModule.MonoModuleStopCoroutine(TimeForAction());
    //    }
    //}

    /// <summary>
    /// 行动的时间到了
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeForAction()
    {
        if (actionStarted) { yield break; }//如果没到可以行动,直接跳出协程
        actionStarted = true;
        //播放敌人接近英雄的攻击动画
        Vector3 enemyPostion = new Vector3(
            heroStateMaschine.EnemyToAttack.transform.position.x + 1.5f,
            heroStateMaschine.EnemyToAttack.transform.position.y,
            heroStateMaschine.EnemyToAttack.transform.position.z);
        while (MoveTowrdsEnemy(enemyPostion))//循环等待1帧
            yield return null;//这个是等待1帧的意思
        //等待
        yield return new WaitForSeconds(0.5f);
        //给与敌人伤害
        DoDamage();
        //回到起始位置的动画
        Vector3 firstPosition = heroStateMaschine.startPosition;
        while (MoveTowrdsStart(firstPosition))//循环等待1帧
            yield return null;//这个是等待1帧的意思
        //从BSM的Performer列表移除
        heroStateMaschine.BM.PerformList.RemoveAt(0);
        //重置BSM->等待
        if (!heroStateMaschine.BM.BattleManagerFSMSystem.CheckCurState(BattleManager.PerformAction.WIN.ToString()) &&
            !heroStateMaschine.BM.BattleManagerFSMSystem.CheckCurState(BattleManager.PerformAction.LOSE.ToString()))
        {
            //等待状态
            heroStateMaschine.BM.BattleManagerFSMSystem.ChangeGameState(BattleManager.PerformAction.WAIT.ToString());
            //重置英雄状态
            heroStateMaschine.cur_colldown = 0f;
            heroStateMaschine.heroFSMSystem.ChangeGameState(HeroStateMaschine.TurnState.PROCESSING.ToString());
        }
        else
        {
            //等待状态
            heroStateMaschine.heroFSMSystem.ChangeGameState(HeroStateMaschine.TurnState.WAITING.ToString());
        }
        //结束协程
        actionStarted = false;
    }

    /// <summary>
    /// 给与伤害
    /// </summary>
    private void DoDamage()
    {
        //TODO 英雄的技能技能伤害没加上
        float calc_damage = heroStateMaschine.hero.curAtk;// + BSM.PerformList[0].choosenAttack.attackDamage;
        heroStateMaschine.EnemyToAttack.GetComponent<EnemyStateMaschine>().TakeDamage(calc_damage);
        DLog.Log($"{heroStateMaschine.gameObject.name}发动了{heroStateMaschine.BM.PerformList[0].choosenAttack.attackName}攻击");
        //Debug.Log($"{heroStateMaschine.gameObject.name}用{BSM.PerformList[0].choosenAttack.attackName}给{EnemyToAttack.name}:{calc_damage}点伤害");
    }

    /// <summary>
    /// 移动英雄 如果英雄没移动到敌人坐标的时候  返回的就是false
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool MoveTowrdsEnemy(Vector3 target)
    {
        return target != (heroStateMaschine.transform.position = Vector3.MoveTowards(heroStateMaschine.transform.position, target, animSpeed * Time.deltaTime));
    }

    /// <summary>
    /// 回到原来的位置
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool MoveTowrdsStart(Vector3 target)
    {
        return target != (heroStateMaschine.transform.position = Vector3.MoveTowards(heroStateMaschine.transform.position, target, animSpeed * Time.deltaTime));
    }
}