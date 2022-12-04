using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelComponent : BaseUIGetComponents_new
{
    public StartPanel startPanel { get; private set; }

    public StartPanelComponent(StartPanel startPanel, GameObject Obj) : base(Obj)
    {
        this.startPanel = startPanel;
    }

    public Button V_loadDataButton { set; get; }
    public Button V_startGameButton { set; get; }
    public Button V_settingButton { set; get; }
    public Button V_quitButton { set; get; }

    public override void Init()
    {
        base.Init();
        OnGetComponent();
        OnAddListener();
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        V_loadDataButton = GetButton("V_loadData");
        V_startGameButton = GetButton("V_startGame");
        V_settingButton = GetButton("V_setting");
        V_quitButton = GetButton("V_quit");
    }

    /// <summary>
    /// 按钮监听
    /// </summary>
    private void OnAddListener()
    {
        V_loadDataButton.onClick.AddListener(startPanel.V_loadDataAddListener);
        V_startGameButton.onClick.AddListener(startPanel.V_startGameAddListener);
        V_settingButton.onClick.AddListener(startPanel.V_settingAddListener);
        V_quitButton.onClick.AddListener(startPanel.V_quitAddListener);
    }
}
