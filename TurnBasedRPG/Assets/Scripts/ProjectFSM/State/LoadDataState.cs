using LogUtils;
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
        GameRoot.Instance.audioMgr = AudioMgr.Instance;
        GameRoot.Instance.prefabMgr = PrefabMgr.Instance;
        GameRoot.Instance.dataMgr = DataMgr.Instance;
        
        DLog.Log("初始化资源完毕");
        //转换模块
        fSMSystem.ChangeGameState(GameState.EnterGame, this);
    }
}
