using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class FSMState
{
    protected FSMSystem fSMSystem { get; set; }

    public FSMState(FSMSystem fSMSystem, object obj = null) => this.fSMSystem = fSMSystem;

    /// <summary>
    /// 进入新状态之前做的事
    /// </summary>
    public virtual void DoEnter(object obj) { }
    /// <summary>
    /// 离开当前状态时做的事
    /// </summary>
    public virtual void DoLeave(object obj) { }
    /// <summary>
    /// 每帧要做的事
    /// </summary>
    public virtual void DOUpdata() { }

}

