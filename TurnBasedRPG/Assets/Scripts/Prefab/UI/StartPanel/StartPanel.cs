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
        //TODO ��ս����ʱ���ȥ����BattlePanel���
        gameRoot.scenesModule.LoadSceneAsyn(ScenesConfig.BattleScene, () =>
        {
            gameRoot.uiModule.ShowPanel<BattlePanel>(ConfigUIPrefab.BattlePanel, E_UI_Layer.Bottom);
            BasePanelExpand.HidePanel(this);
        });
    }

    /// <summary>
    /// ���ð�ť
    /// </summary>
    internal void V_settingAddListener()
    {
        gameRoot.uiModule.ShowPanel<SettingPanel>(ConfigUIPrefab.SettingPanel, E_UI_Layer.Bottom);
    }

    /// <summary>
    /// �˳���ť
    /// </summary>
    internal void V_quitAddListener()
    {
        gameRoot.FSMSystem.ChangeGameState(GameState.LeaveGame, this);
    }
}
