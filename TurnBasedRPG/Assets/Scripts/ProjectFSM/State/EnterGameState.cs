using LogUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGameState : FSMState
{
    private GameRoot gameRoot { get; set; }

    public EnterGameState(FSMSystem fSMSystem) : base(fSMSystem)
    {
        gameRoot = GameRoot.Instance;
    }

    public override void DoEnter(object obj)
    {
        gameRoot.scenesModule.LoadSceneAsyn(Config.MainScenes, () =>
         {
             //gameRoot.prefabMgr.GetPrefab<StartPanel>(PrefabConfig.StartPanel,);

             gameRoot.uiModule.ShowPanel<StartPanel>(PrefabConfig.StartPanel, E_UI_Layer.Bottom);
         });
    }
}
