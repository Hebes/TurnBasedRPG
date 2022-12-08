
using LogUtils;
using UnityEngine;

internal class InitModuleGameState : FSMState
{
    public InitModuleGameState(FSMSystem fSMSystem) : base(fSMSystem)
    {
    }

    public override void DoEnter(object obj)
    {
        bool isLogPrint= PlayerPrefs.GetInt("设置日志开启") == 0;
        
        //Debug模块
        //主动日志模块
        DLog.InitSettings(new LogConfig()
        {
            enableSave= isLogPrint,
            eLoggerType = LoggerType.Unity,
#if !UNITY_EDITOR
            savePath = $"{Application.persistentDataPath}/LogOut/ActiveLog/",
#endif
            savePath = $"{Application.dataPath}/LogOut/ActiveLog/",
            saveName = "Debug主动输出日志.txt",
        });
        //被动日志模块
        PassiveLog passiveLog = PassiveLog.Instance;
        //Event模块
        GameRoot.Instance.eventModule = EventModule.Instance;
        //MonoMgr模块
        GameRoot.Instance.monoModule = MonoModule.Instance;
        //poolMgr模块
        GameRoot.Instance.poolModule = PoolModule.Instance;
        //资源加载模块
        GameRoot.Instance.resModule = ResModule.Instance;
        //场景加载模块
        GameRoot.Instance.scenesModule = ScenesModule.Instance;
        //UI管理模块
        GameRoot.Instance.uiModule = UIModule.Instance;

        DLog.Log("初始化模块完毕");
        //切换模块
        fSMSystem.ChangeGameState(GameState.LoadData, this);
    }
}