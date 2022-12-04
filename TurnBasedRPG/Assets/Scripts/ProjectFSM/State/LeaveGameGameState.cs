using UnityEngine;
using LogUtils;

internal class LeaveGameGameState : FSMState
{
    public LeaveGameGameState(FSMSystem fSMSystem) : base(fSMSystem)
    {
    }

    public override void DoEnter(object obj)
    {
        //离开游戏
#if UNITY_EDITOR//在编辑器模式退出
        UnityEditor.EditorApplication.isPlaying = false;
#else//发布后退出
        Application.Quit();
#endif
        PELog.Log("关闭游戏");
    }
}