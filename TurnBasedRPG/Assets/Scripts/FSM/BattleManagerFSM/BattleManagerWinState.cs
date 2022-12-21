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

internal class BattleManagerWinState : FSMState
{
    public BattleManager battleManager { get; private set; }

    public BattleManagerWinState(FSMSystem fSMSystem, object obj) : base(fSMSystem, obj)
    {
        battleManager = obj as BattleManager;
    }


    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);

        //显示结算面板 需要自己填写
        PanelExpand.ShowTopHint(new HintInfo()
        {
            title = "系统提示",
            countent = "你赢了",
        }, (panel) =>
        {

        });

        for (int i = 0; i < battleManager.HerosInBattle.Count; i++)
            battleManager.HerosInBattle[i].GetComponent<HeroStateMaschine>().heroFSMSystem.ChangeGameState(HeroStateMaschine.TurnState.WAITING.ToString());
        //TODO 如果不跳转场景敌人会继续攻击
        battleManager.battlePanel.ClearHeroBar();
        //加载进入战斗前的场景
        SceneMG.Instance.LoadSceneAfterBattle();
        SceneMG.Instance.sceneMgrFSMSystem.ChangeGameState(SceneMG.GameStates.WORLD_STATE.ToString());
        SceneMG.Instance.enemysToBattleLists.Clear();
    }
}