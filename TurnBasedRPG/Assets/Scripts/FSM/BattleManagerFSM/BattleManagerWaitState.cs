/******************************************
	作者：暗沉
	邮箱：空
	日期：2022-12-06 11:10:14
	功能：战斗界面管理器

	//===============================\
				空
	\===============================//
******************************************/

using LogUtils;

internal class BattleManagerWaitState : FSMState
{
    public BattleManager battleManager { get; private set; }

    public BattleManagerWaitState(FSMSystem fSMSystem, object obj) : base(fSMSystem, obj)
    {
        battleManager = obj as BattleManager;
    }

    public override void DOUpdata()
    {
        base.DOUpdata();
        //行动的执行列表大于0的时候
        if (battleManager.PerformList.Count > 0)
        {
            //DLog.Log("行动的执行列表大于0的时候");
            battleManager.BattleManagerFSMSystem.ChangeGameState(BattleManager.PerformAction.TAKEACTION.ToString());
        }
    }
}