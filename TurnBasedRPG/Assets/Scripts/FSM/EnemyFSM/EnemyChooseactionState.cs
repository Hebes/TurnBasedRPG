using LogUtils;
using UnityEngine;

/// <summary>
/// 怪物选择敌人行动
/// </summary>
internal class EnemyChooseactionState : FSMState
{
    public EnemyStateMaschine enemyStateMaschine { get; private set; }

    public EnemyChooseactionState(FSMSystem fSMSystem, EnemyStateMaschine enemyStateMaschine) : base(fSMSystem)
    {
        this.enemyStateMaschine = enemyStateMaschine;
    }


    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        ChooseAction();
        enemyStateMaschine.enemyFSMSystem.ChangeGameState(EnemyStateMaschine.TurnState.WAITING.ToString());
    }

    /// <summary>
    /// 现在是敌人状态 选择英雄行动
    /// </summary>
    private void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = enemyStateMaschine.enemy.theName;
        myAttack.Type = "Enemy";
        myAttack.AttackersGameObject = enemyStateMaschine.gameObject;
        myAttack.AttackersTarget = enemyStateMaschine.BM.HerosInBattle[Random.Range(0, enemyStateMaschine.BM.HerosInBattle.Count)];//随机敌人
        myAttack.choosenAttack = enemyStateMaschine.enemy.attacks[Random.Range(0, (int)(enemyStateMaschine.enemy.attacks?.Count))];//TODO 怪物选择攻击方式   

        //伤害公式=emeny的enemy.curAtk+选择攻击方式的一种的伤害-对方的防御
        DLog.Log(enemyStateMaschine.name + "选择了：" + myAttack.choosenAttack.attackName + "攻击方式,对" + myAttack.AttackersTarget.name + "造成" + (myAttack.choosenAttack.attackDamage) + "伤害");//+ enemy.curAtk

        //添加到执行行动的列表
        enemyStateMaschine.BM.CollectActions(myAttack);
    }
}