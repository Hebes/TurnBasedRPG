internal class HeroWaitingState : FSMState
{
    public HeroWaitingState(FSMSystem fSMSystem, HeroStateMaschine heroStateMaschine) : base(fSMSystem)
    {
    }
    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        //空闲状态 也就是暂停状态
    }
}