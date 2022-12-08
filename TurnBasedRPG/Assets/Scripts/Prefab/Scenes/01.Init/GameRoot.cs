using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : SingletonAutoMono<GameRoot>
{
    public GameObject Canvas { get; set; }

    public EventModule eventModule { get; set; }
    public MonoModule monoModule { get; set; }
    public PoolModule poolModule { get; set; }
    public ResModule resModule { get; set; }
    public ScenesModule scenesModule { get; set; }
    public UIModule uiModule { get; set; }

    public AudioMgr audioMgr { get; internal set; }
    public DataMgr dataMgr { get; internal set; }
    public PrefabMgr prefabMgr { get; internal set; }

    public FSMSystem FSMSystem { get; set; }

    

    protected override void Awake()
    {
        base.Awake();
        FSMSystem = new FSMSystem();
        FSMSystem.stateDic = new Dictionary<GameState, FSMState>()
        {
            {GameState.None,new NoneGameState(FSMSystem) },
            {GameState.InitModule,new InitModuleGameState(FSMSystem) },
            {GameState.LoadData,new LoadDataState(FSMSystem) },
            {GameState.EnterGame,new EnterGameState(FSMSystem) },
            {GameState.GameOver,new GameOverGameState(FSMSystem) },
            {GameState.LeaveGame,new LeaveGameGameState(FSMSystem) },
        };
        FSMSystem.ChangeGameState(GameState.None, this);

        DontDestroyOnLoad(this);
        Canvas = GameObject.Find("Canvas");
        DontDestroyOnLoad(Canvas);
    }
    private void Update()
    {
        FSMSystem.Update();
    }
}