using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRoot : SingletonAutoMono<GameRoot>
{
    /// <summary>当前状态</summary>
    public enum GameState
    {
        /// <summary>
        /// 初始化模块
        /// </summary>
        InitModule,
        /// <summary>
        /// 加载数据
        /// </summary>
        LoadData,
        /// <summary>
        /// 进入游戏
        /// </summary>
        EnterGame,
        /// <summary>
        /// 游戏失败
        /// </summary>
        GameOver,
        /// <summary>
        /// 离开游戏
        /// </summary>
        LeaveGame,
    }

    public Camera MainCanmera;//摄像机
    public GameObject Canvas;//画布
    public GameObject SceneManager;//场景管理器

    public EventModule eventModule { get; set; }
    public MonoModule monoModule { get; set; }
    public PoolModule poolModule { get; set; }
    public ResModule resModule { get; set; }
    public ScenesModule scenesModule { get; set; }
    public UIModule uiModule { get; set; }

    public AudioMgr audioMgr { get; internal set; }
    public DataMgr dataMgr { get; internal set; }
    public PrefabMgr prefabMgr { get; internal set; }

    public BattleManager battleManager { get; set; }
    public AudioModule audioModule { get; set; }
    public SceneMG sceneManager { get; internal set; }

    public FSMSystem FSMSystem { get; set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        FSMSystem = new FSMSystem();
        FSMSystem.stateDic = new Dictionary<string, FSMState>()
        {
            {GameState.InitModule.ToString(),new InitModuleGameState(FSMSystem,this) },
            {GameState.LoadData.ToString(),new LoadDataState(FSMSystem,this) },
            {GameState.EnterGame.ToString(),new EnterGameState(FSMSystem,this) },
            {GameState.GameOver.ToString(),new GameOverGameState(FSMSystem,this) },
            {GameState.LeaveGame.ToString(),new LeaveGameGameState(FSMSystem,this) },
        };
        FSMSystem.ChangeGameState(GameState.InitModule.ToString(), this);
    }
    public Coroutine MonoModuleStartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    private void Update()
    {
        FSMSystem.Update();
    }

    #region 杂项代码
    ///// <summary>
    ///// 从固定场景开始执行
    ///// </summary>
    //[RuntimeInitializeOnLoadMethod]
    //public static void Initialize()
    //{
    //    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ConfigScenes.Init) return;
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(ConfigScenes.Init);
    //}
    #endregion

}