using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 1.可以提供给外部添加帧更新事件的方法
/// 2.可以提供给外部添加 协程的方
/// 3.变成单例模式
/// </summary>
public class MonoModule : SingletonAutoMono<MonoModule>
{
    private event UnityAction awakeEvent;
    private event UnityAction startEvent;
    private event UnityAction updateEvent;
    private event UnityAction fixUpdateEvent;

    protected override void Awake()
    {
        base.Awake();
        awakeEvent?.Invoke();
    }
    private void Start()
    {
        startEvent?.Invoke();
    }
    private void Update()
    {
        updateEvent?.Invoke();
    }
    private void FixedUpdate()
    {
        fixUpdateEvent?.Invoke();
    }

    //*********************************添加事件监听*********************************
    /// <summary>
    /// 给外部提供的 添加Awake事件的函数
    /// </summary>
    /// <param name="unityAction"></param>
    public void AddAwakeListener(UnityAction unityAction)
    {
        awakeEvent += unityAction;
    }

    /// <summary>
    /// 给外部提供的 添加Start事件的函数
    /// </summary>
    /// <param name="unityAction"></param>
    public void AddStartListener(UnityAction unityAction)
    {
        startEvent += unityAction;
    }

    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    //*********************************移除事件监听*********************************
    /// <summary>
    /// 提供给外部 用于移除帧更新事件函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
    /// <summary>
    /// 提供给外部 用于移除Awake事件函数
    /// </summary>
    /// <param name="unityAction"></param>
    public void RemoveAwakeListener(UnityAction unityAction)
    {
        awakeEvent -= unityAction;
    }

    // <summary>
    /// 给外部提供的 添加Start事件的函数
    /// </summary>
    /// <param name="unityAction"></param>
    public void RemoveStartListener(UnityAction unityAction)
    {
        startEvent -= unityAction;
    }

    //*********************************携程方法的使用*********************************
    public Coroutine MonoModuleStartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
    public Coroutine MonoModuleStartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return StartCoroutine(methodName, value);
    }
    public Coroutine MonoModuleStartCoroutine(string methodName)
    {
        return StartCoroutine(methodName);
    }
    public void MonoModuleStopCoroutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }
    public void MonoModuleStopCoroutine(Coroutine routine)
    {
        StopCoroutine(routine);
    }
    public void MonoModuleStopCoroutine(string methodName)
    {
        StopCoroutine(methodName);
    }
}
