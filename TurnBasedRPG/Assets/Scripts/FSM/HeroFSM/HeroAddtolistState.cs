using UnityEngine;
using LogUtils;

/// <summary>
/// 添加到列表中
/// </summary>
internal class HeroAddtolistState : FSMState
{
    public HeroStateMaschine heroStateMaschine { get; private set; }

    public HeroAddtolistState(FSMSystem fSMSystem, HeroStateMaschine heroStateMaschine) : base(fSMSystem)
    {
        this.heroStateMaschine = heroStateMaschine;
    }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        heroStateMaschine = obj as HeroStateMaschine;
        heroStateMaschine.BM.HerosToManage.Add(heroStateMaschine.gameObject);
        heroStateMaschine.heroFSMSystem.ChangeGameState(HeroStateMaschine.TurnState.WAITING.ToString(), heroStateMaschine);
    }
}