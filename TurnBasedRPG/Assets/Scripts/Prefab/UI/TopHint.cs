using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 消息提示的配置文件
/// </summary>
public class HintInfo
{
    /// <summary>
    /// 标题名称
    /// </summary>
    public string title { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string countent { get; set; }

}

/// <summary>
/// 消息提示
/// </summary>
public class TopHint : MonoBehaviour
{
    #region UI代码 自动化生成
    #region 组件模块
    public Text T_titleText { set; get; }
    public Text T_countentText { set; get; }

    public Transform T_titleTransform { set; get; }
    public Transform T_countentTransform { set; get; }

    #endregion

    #region 获取组件模块
    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        T_titleText = transform.OnGetText("T_title");
        T_countentText = transform.OnGetText("T_countent");

        T_titleTransform = transform.OnGetTransform("T_title");
        T_countentTransform = transform.OnGetTransform("T_countent");
    }
    #endregion
    #endregion

    private CanvasGroup canvasGroup { get; set; }
    /// <summary>
    /// 提示面板默认是不显示的
    /// </summary>
    private float targetAlpha { get; set; } = 0;
    /// <summary>
    /// 渐变速度
    /// </summary>
    private float smoothing { get; set; } = 1;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        OnGetComponent();

    }

    public void SetInfo(UIInfo<TopHint> uIInfo)
    {
        T_titleText.text = (uIInfo.Data as HintInfo).title;
        T_countentText.text = (uIInfo.Data as HintInfo).countent;

        switch (uIInfo.layer)
        {
            case E_UI_Layer.Bottom: break;
            case E_UI_Layer.Mid: break;
            case E_UI_Layer.Top: break;
            case E_UI_Layer.System: transform.SetParent(GameRoot.Instance.uiModule.system, false); break;

            default: break;
        }
        uIInfo.callBack?.Invoke(this);
    }

    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                transform.position = UnityEngine.Vector3.zero;
                canvasGroup.alpha = 1;
                GameRoot.Instance.poolModule.PushObj(gameObject.name, gameObject);
            }
        }
    }
}
