using UnityEngine;
using LogUtils;

internal class LeaveGameGameState : FSMState
{
    public LeaveGameGameState(FSMSystem fSMSystem) : base(fSMSystem)
    {
    }

    public override void DoEnter(object obj)
    {
        //�뿪��Ϸ
#if UNITY_EDITOR//�ڱ༭��ģʽ�˳�
        UnityEditor.EditorApplication.isPlaying = false;
#else//�������˳�
        Application.Quit();
#endif
        PELog.Log("�ر���Ϸ");
    }
}