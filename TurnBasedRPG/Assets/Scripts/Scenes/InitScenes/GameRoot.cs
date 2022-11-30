using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : SingletonMono<GameRoot>
{
    public GameObject Canvas { get; private set; }
    private FSMSystem FSMSystem { get; set; }

    public GameObject gameObject;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        FSMSystem = new FSMSystem();
        FSMSystem.stateDic = new Dictionary<GameState, FSMState>()
        {
            //{GameState.LoadData,new InitModuleGameState(FSMSystem) },
            //{GameState.LoadData,new NoneGameState(FSMSystem) },
            {GameState.LoadData,new LoadDataState(FSMSystem) },
            {GameState.EnterGame,new EnterGameState(FSMSystem) },
            //{GameState.EnterGame,new GameOverGameState(FSMSystem) },
            //{GameState.EnterGame,new LeaveGameGameState(FSMSystem) },
        };
        FSMSystem.ChangeGameState(GameState.LoadData, this);

        //Canvas = GameObject.Find("Canvas");
        //DontDestroyOnLoad(Canvas);
    }
    private void Update()
    {
        FSMSystem.Update();
    }
}