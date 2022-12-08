using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogUtils;

public class BattlePanel : BasePanel
{
    public override void AwakePanel()
    {
        base.AwakePanel();
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
        DLog.Log("πÿ±’¡ÀBattlePanel√Ê∞Â");
    }
}
