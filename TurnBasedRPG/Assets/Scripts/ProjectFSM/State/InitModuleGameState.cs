
using LogUtils;
using UnityEngine;

internal class InitModuleGameState : FSMState
{
    public InitModuleGameState(FSMSystem fSMSystem) : base(fSMSystem)
    {
    }

    public override void DoEnter(object obj)
    {
        bool isLogPrint= PlayerPrefs.GetInt("������־����") == 0;
        
        //Debugģ��
        //������־ģ��
        DLog.InitSettings(new LogConfig()
        {
            enableSave= isLogPrint,
            eLoggerType = LoggerType.Unity,
#if !UNITY_EDITOR
            savePath = $"{Application.persistentDataPath}/LogOut/ActiveLog/",
#endif
            savePath = $"{Application.dataPath}/LogOut/ActiveLog/",
            saveName = "Debug���������־.txt",
        });
        //������־ģ��
        PassiveLog passiveLog = PassiveLog.Instance;
        //Eventģ��
        GameRoot.Instance.eventModule = EventModule.Instance;
        //MonoMgrģ��
        GameRoot.Instance.monoModule = MonoModule.Instance;
        //poolMgrģ��
        GameRoot.Instance.poolModule = PoolModule.Instance;
        //��Դ����ģ��
        GameRoot.Instance.resModule = ResModule.Instance;
        //��������ģ��
        GameRoot.Instance.scenesModule = ScenesModule.Instance;
        //UI����ģ��
        GameRoot.Instance.uiModule = UIModule.Instance;

        DLog.Log("��ʼ��ģ�����");
        //�л�ģ��
        fSMSystem.ChangeGameState(GameState.LoadData, this);
    }
}