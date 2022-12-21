/******************************************
	作者：暗沉
	邮箱：空
	日期：2022-12-06 11:10:14
	功能：战斗界面管理器

	//===============================\
				空
	\===============================//
******************************************/

using UnityEngine;

/// <summary>
/// 采取行动
/// </summary>
internal class BattleManagerTakeactionState : FSMState
{
    public BattleManager battleManager { get; private set; }

    public BattleManagerTakeactionState(FSMSystem fSMSystem, object obj) : base(fSMSystem, obj)
    {
        battleManager = obj as BattleManager;
    }


    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        //发动的攻击者
        GameObject performer = battleManager.PerformList[0].AttackersGameObject;
        //如果是敌人的话
        if (battleManager.PerformList[0].Type.Equals("Enemy"))
        {
            EnemyStateMaschine ESM = performer.GetComponent<EnemyStateMaschine>();
            //攻击前检查被攻击的英雄是否在列表中
            for (int i = 0; i < battleManager.HerosInBattle?.Count; i++)
            {
                if (battleManager.PerformList[0].AttackersTarget == battleManager.HerosInBattle[i])//存在的话
                {
                    ESM.HeroToAttAck = battleManager.PerformList[0].AttackersTarget;
                    ESM.enemyFSMSystem.ChangeGameState(EnemyStateMaschine.TurnState.ACTION.ToString());
                    break;
                }
                else//不存在的话
                {
                    battleManager.PerformList[0].AttackersTarget = battleManager.HerosInBattle[Random.Range(0, battleManager.HerosInBattle.Count)];
                    ESM.HeroToAttAck = battleManager.PerformList[0].AttackersTarget;
                    ESM.enemyFSMSystem.ChangeGameState(EnemyStateMaschine.TurnState.ACTION.ToString());
                }
            }
        }
        //如果是英雄攻击的话
        if (battleManager.PerformList[0].Type.Equals("Hero"))
        {
            HeroStateMaschine HSM = performer.GetComponent<HeroStateMaschine>();
            HSM.EnemyToAttack = battleManager.PerformList[0].AttackersTarget;
            HSM.heroFSMSystem.ChangeGameState(HeroStateMaschine.TurnState.ACTION.ToString());
        }
        battleManager.BattleManagerFSMSystem.ChangeGameState(BattleManager.PerformAction.PERFROMACTION.ToString());
    }
}