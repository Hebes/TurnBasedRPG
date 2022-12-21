using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    /// <summary>
    /// 面板是否活动
    /// </summary>
    public bool isActive { get; private set; }

    /// <summary>
    /// 第一次显示 只执行一次
    /// </summary>
    public virtual void AwakePanel(object obj = null)
    {
        isActive = true;
    }
    /// <summary>
    /// 显示自己 当面板显示一次 就执行一次
    /// </summary>
    public virtual void ShowPanel(object obj = null)
    {
        isActive = true;
        gameObject.SetActive(isActive);
    }
    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HidePanel(object obj = null)
    {
        isActive = false;
        gameObject.SetActive(isActive);
    }
    /// <summary>
    /// 移除面板后需要做的事情
    /// </summary>
    public virtual void RemovePanel(object obj = null)
    {
        Destroy(gameObject);
    }
}
