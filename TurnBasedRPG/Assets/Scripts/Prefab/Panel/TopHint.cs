using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private float smoothing { get; set; } = 2;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        OnGetComponent();

    }

    public void ShowTip(UIInfo<TopHint> uIInfo)
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
        StartCoroutine(Message());
        uIInfo.callBack?.Invoke(this);
    }

    IEnumerator Message() 
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            while (!(Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.1f))
            {
                yield return null;
                Debug.Log(Mathf.Abs(canvasGroup.alpha - targetAlpha));
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
                transform.Translate(Vector3.up * 0.5f* Time.deltaTime);
            }
            
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.1f)
            {
                transform.position = UnityEngine.Vector3.zero;
                canvasGroup.alpha = 1;
                GameRoot.Instance.poolModule.PushObj(gameObject.name, gameObject);
            }
        }
    }

    //private void Update()
    //{
    //    if (canvasGroup.alpha != targetAlpha)
    //    {
    //        transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
    //        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
    //        if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
    //        {
    //            transform.position = UnityEngine.Vector3.zero;
    //            canvasGroup.alpha = 1;
    //            GameRoot.Instance.poolModule.PushObj(gameObject.name, gameObject);
    //        }
    //    }
    //}

    /// <summary>
    /// 提示文字 系统消息
    /// </summary>
    /// <param name="str">提示内容</param>
    /// <param name="stayTime">停留几秒</param>
    /// <param name="moveY">向上飞多高(根据你提示框的高度自定义)</param>//https://blog.csdn.net/yzx5452830/article/details/105119353
    public void ShowTipOfDotween(UIInfo<TopHint> uIInfo, float stayTime = .1f, float moveY = 100f)
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
        Tweener tweener = transform.DOLocalMoveY(transform.localPosition.y + moveY, 0.3f);
        //tweener.SetEase(Ease.InOutBack);//设置动画曲线
        //利用Dotween做动画
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1, 0.2f));
        sequence.AppendInterval(stayTime);
        sequence.Append(canvasGroup.DOFade(0, 0.8f));
        //动画结束后回收
        sequence.OnComplete(() =>
        {
            transform.position = UnityEngine.Vector3.zero;
            GameRoot.Instance.poolModule.PushObj(gameObject.name, gameObject);
        });
        sequence.Play();
        uIInfo.callBack?.Invoke(this);
    }
}
