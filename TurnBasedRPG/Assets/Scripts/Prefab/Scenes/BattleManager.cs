/******************************************
	作者：暗沉
	邮箱：空
	日期：2022-12-06 11:10:14
	功能：战斗界面管理器

	//===============================\
				空
	\===============================//
******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using LogUtils;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// 回合处理类
/// </summary>
[Serializable]
public class HandleTurn
{
    /// <summary>攻击者的名称</summary>
    [Tooltip("攻击者的名称")] public string Attacker;
    /// <summary>类型 </summary>
    [Tooltip("攻击者的种类:Enemy 敌人 Hero:英雄")] public string Type;
    /// <summary>攻击者的物体</summary>
    [Tooltip("发动的攻击者")] public GameObject AttackersGameObject;
    /// <summary>攻击者的目标</summary>
    [Tooltip("攻击者的目标")] public GameObject AttackersTarget;
    /// <summary>执行哪一种攻击</summary>
    [Header("攻击方式")] public BaseAttack choosenAttack;
}

/// <summary>
/// 战斗管理类
/// </summary>
public class BattleManager : MonoBehaviour
{
    /// <summary>执行操作</summary>
    public enum PerformAction
    {
        /// <summary>
        /// 等待
        /// </summary>
        WAIT,
        /// <summary>
        /// 采取行动
        /// </summary>
        TAKEACTION,
        /// <summary>
        /// 执行动作
        /// </summary>
        PERFROMACTION,
        /// <summary>
        /// 检查敌人或者玩家是否存活
        /// </summary>
        CHECKALIVE,
        /// <summary>
        /// 赢
        /// </summary>
        WIN,
        /// <summary>
        /// 输
        /// </summary>
        LOSE
    }
    /// <summary>英雄的GUI选择</summary>
    public enum HeroGUI
    {
        /// <summary>
        /// 激活
        /// </summary>
        ACTIOVATE,
        /// <summary>
        /// 等待
        /// </summary>
        WAITING,
        /// <summary>
        /// 输入1
        /// </summary>
        INPUT1,
        /// <summary>
        /// 输入2
        /// </summary>
        INPUT2,
        /// <summary>
        /// 空
        /// </summary>
        DONE,
    }

    /// <summary>战斗状态机</summary>
    public FSMSystem BattleManagerFSMSystem { get; private set; }
    /// <summary>英雄选择状态机</summary>
    public FSMSystem HeroGUIFSMSystem { get; private set; }
    /// <summary>战斗的Panel</summary>
    public BattlePanel battlePanel { get; private set; }
    public SceneMG sceneManager { get; private set; }


    /// <summary>执行行动的列表</summary>
    public List<HandleTurn> PerformList = new List<HandleTurn>();
    /// <summary>英雄战斗列表</summary>
    [Header("英雄战斗列表")] public List<GameObject> HerosInBattle = new List<GameObject>();
    /// <summary>敌人战斗列表</summary>
    [Header("敌人战斗列表")] public List<GameObject> EnemysInBattle = new List<GameObject>();
    /// <summary>英雄冷却条到了的列表的管理</summary>
    [Header("英雄冷却条到了的列表的管理")] public List<GameObject> HerosToManage = new List<GameObject>();

    /// <summary>攻击按钮</summary>
    public List<Transform> atkBtns = new List<Transform>();
    /// <summary>选择敌人的按钮</summary>
    private List<GameObject> enemytBtns = new List<GameObject>();

    /// <summary>英雄出生点</summary>
    public List<Transform> heroSpawnPoints = new List<Transform>();
    /// <summary>敌人出生点</summary>
    public List<Transform> enemySpawnPoints = new List<Transform>();

    /// <summary>玩家回合需要做的事情的配置</summary>
    public HandleTurn HeroChoise;

    public Transform enemyButtonTF { get; private set; }

    /// <summary>生成敌人按钮的父物体</summary>
    public Transform Spacer;

    private void Awake()
    {
        EnemyStateMaschine BaseEnemy = GameRoot.Instance.prefabMgr.GetPrefab<EnemyStateMaschine>(ConfigUIPrefab.BaseEnemy);
        HeroStateMaschine heroStateMaschine = GameRoot.Instance.prefabMgr.GetPrefab<HeroStateMaschine>(ConfigUIPrefab.BaseHero);
        GameRoot.Instance.battleManager = this;
        sceneManager = GameRoot.Instance.sceneManager;
        //实例化sceneManager里面的敌人的战斗列表
        for (int i = 0; i < sceneManager.enemysToBattleLists.Count; i++)
        {
            EnemyStateMaschine NewEnemy = Instantiate(BaseEnemy, enemySpawnPoints[i]);
            NewEnemy.enemy = sceneManager.enemysToBattleLists[i];
            NewEnemy.name = NewEnemy.enemy.theName + "_" + (i + 1);
            NewEnemy.enemy.theName = NewEnemy.name;
            EnemysInBattle.Add(NewEnemy.gameObject);
        }
        //实例化sceneManager里面的英雄的战斗列表
        for (int i = 0; i < sceneManager.heroBattleLists.Count; i++)
        {
            HeroStateMaschine NewHero = Instantiate(heroStateMaschine, heroSpawnPoints[i]);
            NewHero.hero = sceneManager.heroBattleLists[i];
            NewHero.name = NewHero.hero.theName + "_" + (i + 1);
            NewHero.hero.theName = NewHero.name;
            HerosInBattle.Add(NewHero.gameObject);
        }

        enemyButtonTF = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.TargetButton);
        GameRoot.Instance.uiModule.ShowPanel(new UIInfo<BattlePanel>()
        {
            panelName = ConfigUIPrefab.BattlePanel,
            layer = E_UI_Layer.Top,
            callBack = (obj) => { battlePanel = obj; },
        });
    }
    private void OnDestroy()
    {
        GameRoot.Instance.battleManager = null;
    }
    private void Start()
    {
        //battlePanel.T_HeroPanelSpacerTransform.tfClearChild();
        //battlePanel = GameRoot.Instance.uiModule.GetPanel<BattlePanel>(ConfigUIPrefab.BattlePanel);
        //面板设置关闭
        battlePanel.ActionPanel(false);
        battlePanel.SelectTargetPanel(false);
        battlePanel.MagicPanel(false);

        //战斗状态类
        BattleManagerFSMSystem = new FSMSystem();
        BattleManagerFSMSystem.stateDic = new Dictionary<string, FSMState>()
        {
            {PerformAction.WAIT.ToString(),new BattleManagerWaitState(BattleManagerFSMSystem,this) },
            {PerformAction.TAKEACTION.ToString(),new  BattleManagerTakeactionState(BattleManagerFSMSystem,this) },
            {PerformAction.PERFROMACTION.ToString(),new BattleManagerPerfromactionState(BattleManagerFSMSystem,this) },
            {PerformAction.CHECKALIVE.ToString(),new  BattleManagerCheckaliveState(BattleManagerFSMSystem,this) },
            {PerformAction.WIN.ToString(),new  BattleManagerWinState(BattleManagerFSMSystem,this) },
            {PerformAction.LOSE.ToString(),new  BattleManagerLoseState(BattleManagerFSMSystem,this) },
        };
        BattleManagerFSMSystem.ChangeGameState(PerformAction.WAIT.ToString());

        //玩家UI面板技能选择
        HeroGUIFSMSystem = new FSMSystem();
        HeroGUIFSMSystem.stateDic = new Dictionary<string, FSMState>()
        {
            {HeroGUI.ACTIOVATE.ToString(),new HeroGUIActiovateState(HeroGUIFSMSystem,this) },
            {HeroGUI.WAITING.ToString(),new  HeroGUIWaitingState(HeroGUIFSMSystem,this) },
            {HeroGUI.INPUT1.ToString(),new HeroGUIInput1State(HeroGUIFSMSystem,this) },
            {HeroGUI.INPUT2.ToString(),new  HeroGUIInput2State(HeroGUIFSMSystem,this) },
            {HeroGUI.DONE.ToString(),new  HeroGUIDoneState(HeroGUIFSMSystem,this) },
        };
        HeroGUIFSMSystem.ChangeGameState(HeroGUI.ACTIOVATE.ToString());

        //生成敌人按钮
        EnemyButtons();
    }

    private void Update()
    {
        BattleManagerFSMSystem.Update();
        HeroGUIFSMSystem.Update();
    }

    /// <summary>
    /// 敌人按钮的生成
    /// </summary>
    public void EnemyButtons()
    {

        //cleanup 清理
        foreach (GameObject enemyBtn in enemytBtns) { Destroy(enemyBtn); }
        enemytBtns.Clear();
        //敌人按钮适配Unity 5 Tutorial: Turn Based Battle System #07 - Gui Improvements
        //创建选择敌人按钮
        foreach (var enemy in EnemysInBattle)
        {
            EnemyStateMaschine cur_enemy = enemy.GetComponent<EnemyStateMaschine>();
            Transform newButton = enemyButtonTF.InstantiatePrefa(battlePanel.T_TargetSpacerTransform);//创建敌人选择按钮
            newButton.OnGetText("T_Text").text = $"{cur_enemy.enemy.theName}";
            newButton.OnGetButton().onClick.AddListener(() =>
            {
                GameRoot.Instance.battleManager.Input2(enemy);
                enemy.transform.Find("T_Selector").gameObject.SetActive(false);
            });
            newButton.AddEventTriggerListener(EventTriggerType.PointerEnter, (baseEventData) =>
            {
                enemy.transform.Find("T_Selector").gameObject.SetActive(true);
            });
            newButton.AddEventTriggerListener(EventTriggerType.PointerExit, (baseEventData) =>
            {
                enemy.transform.Find("T_Selector").gameObject.SetActive(false);
            });
            enemytBtns.Add(newButton.gameObject);
        }

        ////cleanup 清理
        //foreach (GameObject enemyBtn in enemytBtns) { Destroy(enemyBtn); }
        //enemytBtns.Clear();
        ////敌人按钮适配Unity 5 Tutorial: Turn Based Battle System #07 - Gui Improvements
        ////创建选择敌人按钮

        //foreach (var enemy in EnemysInBattle)
        //{
        //    EnemyStateMaschine cur_enemy = enemy.GetComponent<EnemyStateMaschine>();
        //    enemyButtonTF.Prefa_EnemySelectButton(new PrefabExpand.PrefabExpandConfig1()
        //    {
        //        name = cur_enemy.enemy.theName,
        //        parent= battlePanel.T_TargetSpacerTransform,
        //        unityAction1 = (go) => 
        //        {
        //            GameRoot.Instance.battleManager.Input2(enemy);
        //            enemy.transform.Find("T_Selector").gameObject.SetActive(false);
        //        },
        //        PointerEnterCallBack = (baseEventData,go) => 
        //        {
        //            enemy.transform.Find("T_Selector").gameObject.SetActive(true);
        //        },
        //        PointerExitCallBack= (baseEventData, go) =>
        //        {
        //            enemy.transform.Find("T_Selector").gameObject.SetActive(false);
        //        },
        //    });
        //    enemytBtns.Add(cur_enemy.gameObject);
        //}
    }

    /// <summary>
    /// 执行行动的列表
    /// </summary>
    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);
    }

    /// <summary>
    /// 物理攻击选择
    /// </summary>
    /// <param name="choosenEnemy"></param>
    public void Input2(GameObject choosenEnemy)
    {
        HeroChoise.AttackersTarget = choosenEnemy;
        HeroGUIFSMSystem.ChangeGameState(HeroGUI.DONE.ToString());

        Debug.Log("敌人可以行动");
        //// 敌人可以行动
        //foreach (var item in EnemysInBattle)
        //{
        //    EnemyStateMaschine enemyStateMaschine = item.GetComponent<EnemyStateMaschine>();
        //    string enemyCurState = enemyStateMaschine.enemyFSMSystem.GetCurState;//怪物当前状态
        //    if (enemyCurState == EnemyStateMaschine.TurnState.WAITING.ToString())//如果是进度条上升的状态的话
        //        item.GetComponent<EnemyStateMaschine>().enemyFSMSystem.ChangeGameState(EnemyStateMaschine.TurnState.PROCESSING.ToString());//就切换到等待状态,英雄操作完毕后在转换回来
        //}

        //HerosInBattle?.ForEach((go)=>
        //{
        //    string heroCurState = go.GetComponent<HeroStateMaschine>().heroFSMSystem.GetCurState;
        //    if (heroCurState == HeroStateMaschine.TurnState.WAITING.ToString())//如果是进度条上升的状态的话
        //        go.GetComponent<HeroStateMaschine>().heroFSMSystem.ChangeGameState(EnemyStateMaschine.TurnState.PROCESSING.ToString());//就切换到等待状态,英雄操作完毕
        //});
    }

    /// <summary>
    /// 清空攻击面板
    /// </summary>
    public void clearAttackPanel()
    {
        //面板设置关闭
        battlePanel.SelectTargetPanel(false);//选择敌人面板
        battlePanel.ActionPanel(false);//攻击面板
        battlePanel.MagicPanel(false);//魔法面板
        //clean the attackpanel 清理攻击面板
        atkBtns?.ForEach((atkBtn) => { Destroy(atkBtn.gameObject); });//对每个元素的操作
        atkBtns.Clear();
    }
}


