using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataState : FSMState
{
    public LoadDataState(FSMSystem fSMSystem) : base(fSMSystem)
    {
    }

    public override void DoEnter(object obj)
    {
        Debug.Log("进入LoadDataState");
    }

    public override void DoLeave(object obj)
    {
        Debug.Log("离开LoadDataState");
    }

    public override void DOUpdata()
    {
        Debug.Log("循环LoadDataState");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fSMSystem.ChangeGameState(GameState.EnterGame,this);
        }
    }
}
