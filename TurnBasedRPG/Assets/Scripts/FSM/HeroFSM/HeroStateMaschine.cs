using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 英雄状态管理
/// </summary>
public class HeroStateMaschine : MonoBehaviour
{
    /// <summary>回合状态</summary>
    public enum TurnState
    {
        /// <summary>
        /// 进度条上升
        /// </summary>
        PROCESSING,
        /// <summary>
        /// 添加到列表中
        /// </summary>
        ADDTOLIST,
        /// <summary>
        /// 等待
        /// </summary>
        WAITING,
        /// <summary>
        /// 选择
        /// </summary>
        SELECTING,
        /// <summary>
        /// 行动
        /// </summary>
        ACTION,
        /// <summary>
        /// 死去的
        /// </summary>
        DEAD,
    }
    /// <summary>战斗管理类</summary>
    public BattleManager BM { get; private set; }
    /// <summary>英雄状态机</summary>
    public FSMSystem heroFSMSystem { get; private set; }
    /// <summary>英雄属性基类</summary>
    public BaseHero hero;

    /// <summary>当前冷却时间</summary>
    public float cur_colldown { get; set; } = 0f;
    /// <summary>最长冷却时间</summary>
    public float max_colldown { get; set; } = 5f;
    /// <summary>冷却版进度条</summary>
    public Image ProgressBar;
    /// <summary>选择器物体 就是角色头上顶的黄色小物体</summary>
    public GameObject T_SelectorTransform { set; get; }
    /// <summary>玩家的行动冷却条 HeroBar</summary>
    public Transform heroProgressBar { get; set; }
    /// <summary>进度条 heroPanel</summary>
    //private HeroBar stats;

    /// <summary>敌人的位置</summary>
    public GameObject EnemyToAttack;
    /// <summary>英雄开始的位置</summary>
    public Vector3 startPosition;

    /// <summary>dead 是否存活</summary>
    public bool isAlive { get; set; } = true;


    #region UI代码
    #region 组件模块
    public Image T_SelectorImage { set; get; }
    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_SelectorImage = transform.OnGetImage("T_Selector");

        T_SelectorTransform = transform.OnGetTransform("T_Selector").gameObject;
    }
    #endregion
    #endregion

    private void Start()
    {
        OnGetComponent();
        CreatHeroPanel();

        //把自己添加进列表中
        BM = GameRoot.Instance.battleManager;

        //设置英雄初始属性
        //获取数据
        //GameRoot.Instance.dataMgr.GetData<CharacterInfo>(GameRoot.Instance.dataMgr.magicSkillInfo)

        //设置初始
        startPosition = transform.position;
        cur_colldown = UnityEngine.Random.Range(0, 2.5f);
        T_SelectorTransform.SetActive(false);

        //创建状态机
        heroFSMSystem = new FSMSystem();
        heroFSMSystem.stateDic = new Dictionary<string, FSMState>()
        {
            {TurnState.PROCESSING.ToString(),new HeroProcessingState(heroFSMSystem,this) },
            {TurnState.ADDTOLIST.ToString(),new  HeroAddtolistState(heroFSMSystem,this) },
            {TurnState.WAITING.ToString(),new HeroWaitingState(heroFSMSystem,this) },
            {TurnState.SELECTING.ToString(),new  HeroSelectingState(heroFSMSystem,this) },
            {TurnState.ACTION.ToString(),new  HeroActionState(heroFSMSystem,this) },
            {TurnState.DEAD.ToString(),new  HeroDeadState(heroFSMSystem,this) },
        };
        heroFSMSystem.ChangeGameState(TurnState.PROCESSING.ToString(), this);
    }

    private void Update()
    {
        heroFSMSystem?.Update();
    }

    public Coroutine MonoModuleStartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }


    /// <summary>
    /// 创建英雄进度条面板
    /// </summary>
    private void CreatHeroPanel()
    {
        Transform T_HeroPanelSpacerTransform = GameRoot.Instance.uiModule.GetPanel<BattlePanel>(ConfigUIPrefab.BattlePanel).T_HeroPanelSpacerTransform;
        //玩家的行动冷却条 HeroBar
        heroProgressBar = GameRoot.Instance.prefabMgr.GetPrefab<Transform>(ConfigUIPrefab.HeroBar, T_HeroPanelSpacerTransform);
        heroProgressBar.name = "HeroBar_" + hero.theName;//设置物体名称
        heroProgressBar.OnGetText("T_Name").text = hero.theName;//设置物体名称
        HerBarInfo(heroProgressBar);
        ProgressBar = heroProgressBar.OnGetImage("T_ProgressBar");
    }

    /// <summary>
    /// 更新HerBar信息
    /// </summary>
    /// <param name="heroProgressBar"></param>
    private void HerBarInfo(Transform heroProgressBar)
    {
        heroProgressBar.OnGetText("T_HPMP").text = $"HP：{hero.curHP}/{hero.baseHP}\nMP：{hero.curMP}/{hero.BaseMP}";
    }

    /// <summary>
    /// 遭受伤害
    /// </summary>
    public void TakeDamage(float getDamageAmount)
    {
        hero.curHP -= getDamageAmount;
        Debug.Log(hero.theName + "受到：" + getDamageAmount + "点伤害,剩余生命值：" + hero.curHP);
        if (hero.curHP <= 0)
        {
            hero.curHP = 0;
            heroFSMSystem.ChangeGameState(TurnState.DEAD.ToString());
        }
        HerBarInfo(heroProgressBar);
    }
}