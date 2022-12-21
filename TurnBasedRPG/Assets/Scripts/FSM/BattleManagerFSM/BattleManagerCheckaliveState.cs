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
/// 检查敌人列表或者玩家列表是否存活
/// </summary>
internal class BattleManagerCheckaliveState : FSMState
{
    public BattleManager battleManager { get; private set; }

    public BattleManagerCheckaliveState(FSMSystem fSMSystem, object obj) : base(fSMSystem, obj)
    {
        battleManager = obj as BattleManager;
    }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        if (battleManager.HerosInBattle.Count < 1)
        {
            //Lose game
            battleManager.BattleManagerFSMSystem.ChangeGameState(BattleManager.PerformAction.LOSE.ToString());
        }
        else if (battleManager.EnemysInBattle.Count < 1)
        {
            //win the battle
            battleManager.BattleManagerFSMSystem.ChangeGameState(BattleManager.PerformAction.WIN.ToString());
        }
        else
        {
            //call function 
            battleManager.clearAttackPanel();
            battleManager.HeroGUIFSMSystem.ChangeGameState(BattleManager.HeroGUI.ACTIOVATE.ToString());//激活面板状态
        }
    }
}