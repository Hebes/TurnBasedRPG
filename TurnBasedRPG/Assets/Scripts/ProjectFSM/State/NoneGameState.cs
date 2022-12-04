using LogUtils;
using UnityEngine;
internal class NoneGameState : FSMState
{
    public NoneGameState(FSMSystem fSMSystem) : base(fSMSystem)
    {
    }

    public override void DoEnter(object obj)
    {
        fSMSystem.ChangeGameState(GameState.InitModule, this);
    }
}