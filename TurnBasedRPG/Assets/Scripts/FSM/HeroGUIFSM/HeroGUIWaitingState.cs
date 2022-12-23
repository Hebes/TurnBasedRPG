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

internal class HeroGUIWaitingState : FSMState
{
    public BattleManager battleManager { get; private set; }

    public HeroGUIWaitingState(FSMSystem fSMSystem, BattleManager battleManager) : base(fSMSystem)
    {
        this.battleManager = battleManager;
    }
    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        
    }

    public override void DOUpdata()
    {
        base.DOUpdata();
        //敌人停止行动  
        //TODO需要修改怪物暂停的
        foreach (var item in battleManager.EnemysInBattle)
        {
            EnemyStateMaschine enemyStateMaschine = item.GetComponent<EnemyStateMaschine>();
            string enemyCurState = enemyStateMaschine.enemyFSMSystem.GetCurState;//怪物当前状态
            if (enemyCurState == EnemyStateMaschine.TurnState.WAITING.ToString()) continue;
            if (enemyCurState == EnemyStateMaschine.TurnState.PROCESSING.ToString())//如果是进度条上升的状态的话
                item.GetComponent<EnemyStateMaschine>().enemyFSMSystem.ChangeGameState(EnemyStateMaschine.TurnState.WAITING.ToString());//就切换到等待状态,英雄操作完毕后在转换回来
        }
    }
}