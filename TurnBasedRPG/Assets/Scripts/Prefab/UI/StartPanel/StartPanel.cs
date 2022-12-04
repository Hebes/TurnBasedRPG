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
    /// ���ش浵
    /// </summary>
    internal void V_loadDataAddListener()
    {
    }

    /// <summary>
    /// ��ʼ��Ϸ
    /// </summary>
    internal void V_startGameAddListener()
    {
    }

    /// <summary>
    /// ���ð�ť
    /// </summary>
    internal void V_settingAddListener()
    {
        gameRoot.uiModule.ShowPanel<SettingPanel>(PrefabConfig.SettingPanel, E_UI_Layer.Bottom);
    }

    /// <summary>
    /// �˳���ť
    /// </summary>
    internal void V_quitAddListener()
    {
        gameRoot.FSMSystem.ChangeGameState(GameState.LeaveGame, this);
    }
}
