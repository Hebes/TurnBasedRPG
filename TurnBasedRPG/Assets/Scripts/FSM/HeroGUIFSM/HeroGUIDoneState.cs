/******************************************
	作者：暗沉
	邮箱：空
	日期：2022-12-06 11:10:14
	功能：战斗界面管理器

	//===============================\
				空
	\===============================//
******************************************/

internal class HeroGUIDoneState : FSMState
{
    public BattleManager battleManager { get; private set; }

    public HeroGUIDoneState(FSMSystem fSMSystem, BattleManager battleManager) : base(fSMSystem)
    {
		this.battleManager = battleManager;
	}
    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
		HeroInputDone();
	}

    /// <summary>
    /// 玩家输入完毕后
    /// </summary>
    private void HeroInputDone()
    {
        //添加到执行行动的列表
        battleManager.PerformList.Add(battleManager.HeroChoise);
        //清空面板
        battleManager.clearAttackPanel();
        //冷却完毕的英雄的选择图标
        battleManager.HerosToManage[0].transform.Find("T_Selector").gameObject.SetActive(false);
        //冷却完毕列表移除已经操作的英雄
        battleManager.HerosToManage.RemoveAt(0);
        //重新切换到GUI操作面板状态，判断是否还有操作的英雄
        battleManager.HeroGUIFSMSystem.ChangeGameState(BattleManager.HeroGUI.ACTIOVATE.ToString());
    }
}