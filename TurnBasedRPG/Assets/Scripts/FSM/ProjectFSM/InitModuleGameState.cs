
using LogUtils;
using System;
using System.Collections;
using UnityEngine;

internal class InitModuleGameState : FSMState
{
    public InitModuleGameState(FSMSystem fSMSystem, GameRoot gameRoot) : base(fSMSystem)
    {
        this.gameRoot = gameRoot;
    }
    /// <summary>
    /// 加载面板的引用
    /// </summary>
    public LoadPanel loadPanel { get; private set; }
    public GameRoot gameRoot { get; private set; }
    public bool isLogPrint { get; private set; }

    float progress = 0;

    public override void DoEnter(object obj)
    {
        isLogPrint = PlayerPrefs.GetInt("设置日志开启") == 0;

        //UI管理模块
        GameRoot.Instance.uiModule = UIModule.Instance;
        GameRoot.Instance.uiModule.ShowPanel<LoadPanel>(new UIInfo<LoadPanel>()
        {
            panelName = ConfigUIPrefab.LoadPanel,
            layer = E_UI_Layer.System,
            callBack = (loadPanel) =>
            {
                this.loadPanel = loadPanel;

                gameRoot.MonoModuleStartCoroutine(Load());

            },
        });
    }

    IEnumerator Load()
    {
        loadPanel.UpdataProgress(progress, "开始初始化模块");
        //Debug模块
        //主动日志模块
        DLog.InitSettings(new LogConfig()
        {
            enableSave = isLogPrint,
            eLoggerType = LoggerType.Unity,
#if !UNITY_EDITOR
            //savePath = $"{Application.persistentDataPath}/LogOut/ActiveLog/",
#endif
            savePath = $"{Application.dataPath}/LogOut/ActiveLog/",
            saveName = "Debug主动输出日志.txt",
        });
        yield return new WaitForSeconds(.1f);
        progress += (1f / 8f);
        loadPanel.UpdataProgress(progress, "主动日志模块加载完毕");
        //被动日志模块
        PassiveLog passiveLog = PassiveLog.Instance; passiveLog.name = "被动日志模块";
        yield return new WaitForSeconds(.1f);
        progress += (1f / 8f);
        loadPanel.UpdataProgress(progress, "被动日志模块加载完毕");
        //Event模块
        GameRoot.Instance.eventModule = EventModule.Instance;
        yield return new WaitForSeconds(.1f);
        progress += (1f / 8f);
        loadPanel.UpdataProgress(progress, "Event模块加载完毕");
        //MonoMgr模块
        GameRoot.Instance.monoModule = MonoModule.Instance; GameRoot.Instance.monoModule.name = "MonoMgr模块";
        yield return new WaitForSeconds(.1f);
        progress += (1f / 8f);
        loadPanel.UpdataProgress(progress, "MonoMgr模块加载完毕");
        //poolMgr模块
        GameRoot.Instance.poolModule = PoolModule.Instance; GameRoot.Instance.poolModule.name = "PoolMgr模块";
        yield return new WaitForSeconds(.1f);
        progress += (1f / 8f);
        loadPanel.UpdataProgress(progress, "poolMgr模块加载完毕");
        //资源加载模块
        GameRoot.Instance.resModule = ResModule.Instance; GameRoot.Instance.resModule.name = "资源加载模块";
        yield return new WaitForSeconds(.1f);
        progress += (1f / 8f);
        loadPanel.UpdataProgress(progress, "资源加载模块加载完毕");
        //场景加载模块
        GameRoot.Instance.scenesModule = ScenesModule.Instance; GameRoot.Instance.scenesModule.name = "场景加载模块";
        yield return new WaitForSeconds(.1f);
        progress += (1f / 8f);
        loadPanel.UpdataProgress(progress, "场景加载模块加载完毕");
        //音频模块
        GameRoot.Instance.audioModule = AudioModule.Instance; GameRoot.Instance.audioModule.name = "音频模块";
        yield return new WaitForSeconds(.1f);
        progress += (1f / 8f);
        loadPanel.UpdataProgress(progress, "音频模块加载完毕");
        loadPanel.UpdataProgress(progress, "初始化模块完毕");
        //切换模块
        fSMSystem.ChangeGameState(GameRoot.GameState.LoadData.ToString(), this);
    }
}