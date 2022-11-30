using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGameState : FSMState
{
    public EnterGameState(FSMSystem fSMSystem) : base(fSMSystem)
    {
    }

    public override void DoEnter(object obj)
    {
        Debug.Log("进入EnterGameState");
    }

    public override void DoLeave(object obj)
    {
        Debug.Log("离开LoadDataState");
    }

    public override void DOUpdata()
    {
        Debug.Log("循环LoadDataState");
    }
}
