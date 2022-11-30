using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    public bool isActive { get; private set; }= false;

    /// <summary>
    /// 第一次显示 只执行一次
    /// </summary>
    public void AwakePanel()
    {
        isActive = true;
    }
    /// <summary>
    /// 显示自己 当面板显示一次 就执行一次
    /// </summary>
    public virtual void ShowPanel() 
    {
        isActive = true;
    }
    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HidePanel() 
    {
        isActive = false;
    }
    /// <summary>
    /// 移除面板后需要做的事情
    /// </summary>
    public virtual void RemovePanel() 
    {
        isActive = false;
    }
}
