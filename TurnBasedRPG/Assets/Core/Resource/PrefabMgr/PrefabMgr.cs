using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogUtils;
using System.Linq;

/// <summary>
/// �������ģ��
/// </summary>
public class PrefabMgr : BaseManager<PrefabMgr>
{
    //TODO ��д��ͬ�������ز�ͬ����
    //public string path { get; } = "";

    private List<Transform> prefabs { get; set; }

    public PrefabMgr()
    {
        prefabs = new List<Transform>();
        DLog.Log("��ʼ����������");
        LoadPrefab("Prefabs");
    }

    /// <summary>
    /// ��������,�л�����ʱͬ�����������������������ز�ͬ����������
    /// </summary>
    //private void LoadUI()
    //{
    //    prefabs.Clear();
    //    Transform[] transforms = Resources.LoadAll<Transform>("Prefabs/UI");
    //    prefabs.AddRange(transforms);
    //}

    /// <summary>
    /// ����ͨ������
    /// </summary>
    private void LoadPrefab(string path)
    {
        Transform[] transforms = Resources.LoadAll<Transform>(path);
        prefabs.AddRange(transforms);
    }

    /// <summary>
    /// ��ȡprefab
    /// </summary>
    //public T GetUI<T>(string name, Transform parent = null) where T : Component
    //{
    //    Transform prefab = prefabs.Find((obj) => { return obj.name.Equals(name); });
    //    if (prefab != null)
    //        return prefab.GetComponent<T>() == null ? prefab.gameObject.AddComponent<T>() : prefab.GetComponent<T>();
    //    PELog.Log("û�и�����");
    //    return null;
    //}

    /// <summary>
    /// ��ȡprefab��δʵ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetPrefab<T>(string name) where T : Component
    {
        Transform prefab = prefabs.Find((obj) => { return obj.name.Equals(name); });
        if (prefab != null)
            return prefab.GetComponent<T>() == null ? prefab.gameObject.AddComponent<T>() : prefab.GetComponent<T>();
        DLog.Log("û�и�����");
        return null;
    }

    /// <summary>
    /// ��ȡprefab��ʵ������
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
        DLog.Log("û�и�����");
        return null;
    }

    /// <summary>
    /// ���¼������壨�л�����ʱʹ�ã����б�Ҫ
    /// </summary>
    /// <param name="path"></param>
    public void ReloadPrefab(string path)
    {
        prefabs.Clear();
        LoadPrefab(path);
    }
}
