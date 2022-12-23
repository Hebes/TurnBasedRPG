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
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 英雄操作面板激活
/// </summary>
internal class HeroGUIActiovateState : FSMState
{
    public BattleManager battleManager { get; private set; }

    public HeroGUIActiovateState(FSMSystem fSMSystem, BattleManager battleManager) : base(fSMSystem)
    {
        this.battleManager = battleManager;
    }

    //物理攻击
    private Transform actionButton { get; set; }//物理攻击按钮模板
    public Transform actionSpacer { get; set; }
    //魔法攻击
    public Transform magicButton { get; set; }//魔法攻击按钮模板
    public Transform magicSpacer { get; set; }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        //攻击按钮
        actionSpacer = battleManager.battlePanel.T_ActionSpacerTransform;
        actionButton = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.AttackButton);
        //魔法按钮
        magicSpacer = battleManager.battlePanel.T_MagicSpacerTransform;
        magicButton = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.MagicButton);//攻击按钮
    }

    public override void DOUpdata()
    {
        base.DOUpdata();
        if (battleManager.HerosToManage.Count > 0)//英雄的冷却条数量大于0的时候->有英雄冷却条已经到顶了，可以行动
        {
            battleManager.HerosToManage[0].transform.Find("T_Selector").gameObject.SetActive(true);
            battleManager.HeroChoise = new HandleTurn();
            battleManager.battlePanel.ActionPanel(true);
            
            //populate action buttons 填充操作按钮
            CreateAttackButtons();
            battleManager.HeroGUIFSMSystem.ChangeGameState(BattleManager.HeroGUI.WAITING.ToString());
        }
    }

    /// <summary>
    /// 创建action buttons(创建攻击按钮)
    /// </summary>
    private void CreateAttackButtons()
    {
        DLog.Log("创建攻击按钮");
        //物理攻击
        Transform AttackButton = GameObject.Instantiate(actionButton, actionSpacer);
        AttackButton.OnGetText("Text").text = "攻击";
        AttackButton.GetComponent<Button>().onClick.AddListener(() => { Input1(); });
        battleManager.atkBtns.Add(AttackButton);

        //魔法攻击
        Transform MagicAttackButton = GameObject.Instantiate(magicButton, actionSpacer);
        MagicAttackButton.OnGetText("Text").text = "魔法";
        MagicAttackButton.GetComponent<Button>().onClick.AddListener(() => Input3());
        battleManager.atkBtns.Add(MagicAttackButton);
        //如果魔法攻击的技能大于0
        if (battleManager.HerosToManage[0].GetComponent<HeroStateMaschine>().hero.MagicAttacks.Count > 0)
        {
            //循环生成魔法技能的按钮
            foreach (BaseAttack magicAtk in battleManager.HerosToManage[0].GetComponent<HeroStateMaschine>().hero.MagicAttacks)
            {
                Transform MagicButton = GameObject.Instantiate(magicButton, magicSpacer);
                MagicButton.OnGetText("Text").text = magicAtk.attackName;
                MagicButton.GetComponent<Button>().onClick.AddListener(() => { Input4(magicAtk); });
                battleManager.atkBtns.Add(MagicButton);
            }
        }
        else
        {
            //按钮组件不可交互
            MagicAttackButton.GetComponent<Button>().interactable = false;
        }
    }

    /// <summary>
    /// 玩家回合进行的操作->物理技能
    /// </summary>
    public void Input1()
    {
        battleManager.HeroChoise.Attacker = battleManager.HerosToManage[0].name;//攻击者的名称
        battleManager.HeroChoise.AttackersGameObject = battleManager.HerosToManage[0];//攻击者的物体
        battleManager.HeroChoise.Type = "Hero";//攻击者的类型
        battleManager.HeroChoise.choosenAttack = battleManager.HerosToManage[0].GetComponent<HeroStateMaschine>().hero.attacks[0];//执行攻击的名称
        battleManager.battlePanel.ActionPanel(false);//选择哪种攻击的面板
        battleManager.battlePanel.SelectTargetPanel(true);//选择敌人的面板
    }

    /// <summary>
    /// 切换到魔法攻击
    /// switching to magic attacks 
    /// </summary>
    public void Input3()
    {
        battleManager.battlePanel.ActionPanel(false);
        battleManager.battlePanel.MagicPanel(true);
    }

    /// <summary>
    /// 玩家回合进行的操作->魔法技能
    /// choosen magic attack
    /// </summary>
    /// <param name="choosenMagic">魔法攻击设置</param>
    public void Input4(BaseAttack choosenMagic)
    {
        battleManager.HeroChoise.Attacker = battleManager.HerosToManage[0].name;
        battleManager.HeroChoise.AttackersGameObject = battleManager.HerosToManage[0];
        battleManager.HeroChoise.Type = "Hero";
        battleManager.HeroChoise.choosenAttack = choosenMagic;
        battleManager.battlePanel.MagicPanel(false);//魔法攻击界面
        battleManager.battlePanel.SelectTargetPanel(true);//选择敌人的面板
    }
}