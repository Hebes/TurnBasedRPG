using UnityEngine.Events;

public static class PanelExpand
{
    public static void HidePanel(this BasePanel basePanel)
    {
        UIModule.Instance.HidePanel(basePanel.name);
    }
    public static void HidePanel(string name)
    {
        UIModule.Instance.HidePanel(name);
    }

    /// <summary>
    /// 显示提示面板
    /// </summary>
    /// <param name="data">传入的数据</param>
    /// <param name="callBack">回调函数</param>
    public static void ShowTopHint(HintInfo info, UnityAction<TopHint> callBack = null)
    {
        GameRoot.Instance.poolModule.GetObj($"Prefabs/UI/{ConfigUIPrefab.TopHint}", (obj) =>
        {
            TopHint topHint = obj.GetComponent<TopHint>();
            if (topHint == null) { topHint = obj.AddComponent<TopHint>(); }
            topHint.SetInfo(new UIInfo<TopHint>()
            {
                panelName = ConfigUIPrefab.TopHint,
                layer = E_UI_Layer.System,
                Data = info,
                callBack = callBack,
            });
        });
    }
}
