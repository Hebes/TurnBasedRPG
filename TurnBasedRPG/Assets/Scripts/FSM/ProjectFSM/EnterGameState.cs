using LogUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGameState : FSMState
{
    public EnterGameState(FSMSystem fSMSystem, GameRoot gameRoot) : base(fSMSystem)
    {
        this.gameRoot = gameRoot;
    }

    public GameRoot gameRoot { get; }

    public override void DoEnter(object obj)
    {
        
        gameRoot.sceneManager = SceneMG.Instance;
        gameRoot.sceneManager.SceneManagerInit();

        GameRoot.Instance.uiModule.HidePanel(ConfigUIPrefab.LoadPanel);

        GameRoot.Instance.uiModule.ShowPanel<StartPanel>(new UIInfo<StartPanel>()
        {
            panelName = ConfigUIPrefab.StartPanel,
            layer = E_UI_Layer.Bottom,
        });
    }
}
