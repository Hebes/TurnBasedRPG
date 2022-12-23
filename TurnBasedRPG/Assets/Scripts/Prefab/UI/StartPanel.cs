using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogUtils;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel
{
    //**********************************************UI**********************************************
    #region UI代码 自动化生成
    #region 组件模块
    public Button T_loadDataButton { set; get; }
    public Button T_startGameButton { set; get; }
    public Button T_settingButton { set; get; }
    public Button T_quitButton { set; get; }
    public Button T_testBattleButton { set; get; }

    public Image T_loadDataImage { set; get; }
    public Image T_startGameImage { set; get; }
    public Image T_settingImage { set; get; }
    public Image T_quitImage { set; get; }
    public Image T_testBattleImage { set; get; }

    public Transform T_loadDataTransform { set; get; }
    public Transform T_startGameTransform { set; get; }
    public Transform T_settingTransform { set; get; }
    public Transform T_quitTransform { set; get; }
    public Transform T_testBattleTransform { set; get; }

    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_loadDataButton = transform.OnGetButton("T_loadData");
        T_startGameButton = transform.OnGetButton("T_startGame");
        T_settingButton = transform.OnGetButton("T_setting");
        T_quitButton = transform.OnGetButton("T_quit");
        T_testBattleButton = transform.OnGetButton("T_testBattle");

        T_loadDataImage = transform.OnGetImage("T_loadData");
        T_startGameImage = transform.OnGetImage("T_startGame");
        T_settingImage = transform.OnGetImage("T_setting");
        T_quitImage = transform.OnGetImage("T_quit");
        T_testBattleImage = transform.OnGetImage("T_testBattle");

        T_loadDataTransform = transform.OnGetTransform("T_loadData");
        T_startGameTransform = transform.OnGetTransform("T_startGame");
        T_settingTransform = transform.OnGetTransform("T_setting");
        T_quitTransform = transform.OnGetTransform("T_quit");
        T_testBattleTransform = transform.OnGetTransform("T_testBattle");

    }
    #endregion

    #region 按钮监听模块
    /// <summary>
    /// 按钮监听
    /// </summary>
    private void OnAddListener()
    {
        T_loadDataButton.onClick.AddListener(T_loadDataAddListener);
        T_startGameButton.onClick.AddListener(T_startGameAddListener);
        T_settingButton.onClick.AddListener(T_settingAddListener);
        T_quitButton.onClick.AddListener(T_quitAddListener);
        T_testBattleButton.onClick.AddListener(T_testBattleAddListener);
    }


    #endregion

    #endregion


    //**********************************************逻辑**********************************************
    public GameRoot gameRoot { get; private set; }

    public override void AwakePanel(object obj)
    {
        base.AwakePanel(obj);
        OnGetComponent();
        OnAddListener();
        gameRoot = GameRoot.Instance;
        Effer();
    }

    /// <summary>
    /// 退出按钮
    /// </summary>
    private void T_quitAddListener()
    {
        gameRoot.FSMSystem.ChangeGameState(GameRoot.GameState.LeaveGame.ToString(), this);
    }

    /// <summary>
    /// 设置按钮
    /// </summary>
    private void T_settingAddListener()
    {
        gameRoot.uiModule.ShowPanel(new UIInfo<SettingPanel>()
        {
            panelName = ConfigUIPrefab.SettingPanel,
            layer = E_UI_Layer.System,
        });
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    private void T_startGameAddListener()
    {
        gameRoot.scenesModule.LoadSceneAsyn(ConfigScenes.worldScene, () =>
         {
             PanelExpand.HidePanel(ConfigUIPrefab.StartPanel);
         });
    }

    /// <summary>
    /// 加载存档
    /// </summary>
    private void T_loadDataAddListener()
    {
        Debug.Log("加载存档");
        PanelExpand.SHowTopHintOfDotween(new HintInfo() 
        {
            title = "系统提示111",
            countent = "你输了",
        });

        //PanelExpand.ShowTopHint(new HintInfo()
        //{
        //    title = "系统提示111",
        //    countent = "你输了",
        //}, (panel) => { Debug.Log("显示了TopHint面板"); });
    }

    /// <summary>
    /// 测试直接战斗
    /// </summary>
    private void T_testBattleAddListener()
    {
        //TODO 在战斗的时候才去加载BattlePanel面板

        //gameRoot.scenesModule.LoadSceneAsyn(ScenesConfig.BattleScene, () =>
        //{
        //    BasePanelExpand.HidePanel(ConfigUIPrefab.StartPanel);
        //});
        //GameRoot.Instance.scenesModule.LoadScene(ConfigScenes.BattleScene, null);
        gameRoot.scenesModule.LoadScene(ConfigScenes.BattleScene, () =>
        {
            PanelExpand.HidePanel(ConfigUIPrefab.StartPanel);
        });

        //gameRoot.uiModule.ShowPanel<BattlePanel>(ConfigUIPrefab.BattlePanel, E_UI_Layer.Bottom, (obj) =>
        //{
        //    gameRoot.scenesModule.LoadSceneAsyn(ConfigScenes.BattleScene, () =>
        //    {
        //        BasePanelExpand.HidePanel(ConfigUIPrefab.StartPanel);
        //    });
        //});
    }

    //**********************************************动画效果**********************************************

    /// <summary>
    /// 动画效果
    /// </summary>
    private void Effer()
    {
        List<Transform> transforms = new List<Transform>();
        transforms.Add(T_loadDataTransform);
        transforms.Add(T_startGameTransform);
        transforms.Add(T_settingTransform);
        transforms.Add(T_quitTransform);
        transforms.Add(T_testBattleTransform);
        StartCoroutine(ItemAnimation(transforms));
    }

    IEnumerator ItemAnimation(List<Transform> transforms)
    {
        transforms.ForEach((transformTemp => { transformTemp.localScale = Vector3.zero; }));
        foreach (var item in transforms)
        {
            item.transform.DOScale(1f, 1f).SetEase(Ease.Unset);
            yield return new WaitForSeconds(.2f);
        }
    }
}
