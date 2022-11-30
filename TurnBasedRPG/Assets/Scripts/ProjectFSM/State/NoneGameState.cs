using UnityEngine;
using LogUtils;
internal class NoneGameState : FSMState
{
    public NoneGameState(FSMSystem fSMSystem) : base(fSMSystem)
    {
    }

    public override void DoEnter(object obj)
    {
        PELog.InitSettings();
        //PELog.Log();
    }

    public override void DoLeave(object obj)
    {
    }

    public override void DOUpdata()
    {
    }
}