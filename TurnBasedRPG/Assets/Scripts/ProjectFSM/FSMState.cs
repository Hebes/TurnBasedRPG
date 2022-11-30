using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 当前状态
/// </summary>
public enum GameState
{
    /// <summary>
    /// 空
    /// </summary>
    None,
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

public abstract class FSMState
{
    protected GameState uiState;
    public GameState StateID { get { return uiState; } }
    protected FSMSystem fSMSystem;

    public FSMState(FSMSystem fSMSystem) => this.fSMSystem = fSMSystem;


    /// <summary>
    /// 进入新状态之前做的事
    /// </summary>
    public abstract void DoEnter(object obj);
    /// <summary>
    /// 离开当前状态时做的事
    /// </summary>
    public abstract void DoLeave(object obj);
    /// <summary>
    /// 每帧要做的事
    /// </summary>
    public abstract void DOUpdata();
}

