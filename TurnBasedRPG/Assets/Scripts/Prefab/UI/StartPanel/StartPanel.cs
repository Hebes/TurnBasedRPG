using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogUtils;

public class StartPanel : BasePanel
{
    public GameRoot gameRoot { get; private set; }
    private StartPanelComponent panelComponent { get; set; }

    public override void AwakePanel()
    {
        base.AwakePanel();
        gameRoot = GameRoot.Instance;
        panelComponent = new StartPanelComponent(this, gameObject);
        panelComponent.Init();
    }

    /// <summary>
    /// 加载存档
    /// </summary>
    internal void V_loadDataAddListener()
    {
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    internal void V_startGameAddListener()
    {
        //TODO 在战斗的时候才去加载BattlePanel面板
        gameRoot.scenesModule.LoadSceneAsyn(ScenesConfig.BattleScene, () =>
        {
            gameRoot.uiModule.ShowPanel<BattlePanel>(ConfigUIPrefab.BattlePanel, E_UI_Layer.Bottom);
            BasePanelExpand.HidePanel(this);
        });
    }

    /// <summary>
    /// 设置按钮
    /// </summary>
    internal void V_settingAddListener()
    {
        gameRoot.uiModule.ShowPanel<SettingPanel>(ConfigUIPrefab.SettingPanel, E_UI_Layer.Bottom);
    }

    /// <summary>
    /// 退出按钮
    /// </summary>
    internal void V_quitAddListener()
    {
        gameRoot.FSMSystem.ChangeGameState(GameState.LeaveGame, this);
    }
}
