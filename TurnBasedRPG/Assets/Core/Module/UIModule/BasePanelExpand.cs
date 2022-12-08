using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BasePanelExpand
{
    public static void HidePanel(this BasePanel basePanel)
    {
        UIModule.Instance.HidePanel(basePanel.name);
    }
    public static void HidePanel(string name)
    {
        UIModule.Instance.HidePanel(name);
    }
}
