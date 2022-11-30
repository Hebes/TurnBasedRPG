using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    public Dictionary<GameState, FSMState> stateDic;
    public GameState gameState;
    private FSMState currentState;

    /// <summary>
    /// 更新npc的动作
    /// </summary>
    public void Update()
    {
        currentState.DOUpdata();
    }

    /// <summary>
    /// 执行过渡条件满足时对应状态该做的事
    /// </summary>
    public void ChangeGameState(GameState gameState, object obj)
    {
        if (!stateDic.ContainsKey(gameState))
        {
            Debug.LogError("在状态机里面不存在状态" + gameState + ",无法进行状态转换");
            return;
        }
        currentState = stateDic[gameState];
        this.gameState = currentState.StateID;
        currentState.DoLeave(obj);
        currentState.DoEnter(obj);
    }
}
