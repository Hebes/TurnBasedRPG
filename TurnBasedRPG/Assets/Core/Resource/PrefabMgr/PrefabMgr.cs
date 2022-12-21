using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogUtils;
using System.Linq;

/// <summary>
/// 物体管理模块
/// </summary>
public class PrefabMgr : BaseManager<PrefabMgr>
{
    //TODO 编写不同场景加载不同物体
    //public string path { get; } = "";

    private List<Transform> prefabs { get; set; }

    public PrefabMgr()
    {
        prefabs = new List<Transform>();
        LoadPrefab("Prefabs");
    }

    /// <summary>
    /// 加载物体,切换场景时同构这个方法调用这个方法加载不同场景的物体
    /// </summary>
    //private void LoadUI()
    //{
    //    prefabs.Clear();
    //    Transform[] transforms = Resources.LoadAll<Transform>("Prefabs/UI");
    //    prefabs.AddRange(transforms);
    //}

    /// <summary>
    /// 加载通用物体
    /// </summary>
    private void LoadPrefab(string path)
    {
        Transform[] transforms = Resources.LoadAll<Transform>(path);
        prefabs.AddRange(transforms);
    }

    /// <summary>
    /// 获取prefab
    /// </summary>
    //public T GetUI<T>(string name, Transform parent = null) where T : Component
    //{
    //    Transform prefab = prefabs.Find((obj) => { return obj.name.Equals(name); });
    //    if (prefab != null)
    //        return prefab.GetComponent<T>() == null ? prefab.gameObject.AddComponent<T>() : prefab.GetComponent<T>();
    //    PELog.Log("没有该物体");
    //    return null;
    //}

    /// <summary>
    /// 获取prefab（未实例化）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetPrefab<T>(string name) where T : Component
    {
        Transform prefab = prefabs.Find((obj) => { return obj.name.Equals(name); });
        if (prefab != null)
            return prefab.GetComponent<T>() == null ? prefab.gameObject.AddComponent<T>() : prefab.GetComponent<T>();
        DLog.Log("没有该物体");
        return null;
    }

    /// <summary>
    /// 获取prefab（实例化）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public T GetPrefab<T>(string name, Transform parent) where T : Component
    {
        Transform prefab = prefabs.Find((obj) => { return obj.name.Equals(name); });
        if (prefab != null)
        {
            Transform obj = GameObject.Instantiate(prefab, parent);
            return obj.GetComponent<T>() == null ? obj.gameObject.AddComponent<T>() : obj.GetComponent<T>();
        }
        DLog.Log("没有该物体");
        return null;
    }

    /// <summary>
    /// 重新加载物体（切换场景时使用）如有必要
    /// </summary>
    /// <param name="path"></param>
    public void ReloadPrefab(string path)
    {
        prefabs.Clear();
        LoadPrefab(path);
    }
}
