using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class TransformExpand
{
    //*********************************组件查找拓展1*********************************
    /// <summary>
    /// 未知层级,查找后代指定名称的变换组件。
    /// </summary>
    /// <param name="currentTF">当前变换组件</param>
    /// <param name="childName">后代物体名称</param>
    /// <returns></returns>
    private static Transform tfGet(this Transform transform, string childName)
    {
        //递归:方法内部又调用自身的过程。
        //1.在子物体中查找
        Transform childTF = transform.Find(childName);
        if (childTF != null)
            return childTF;
        for (int i = 0; i < transform.childCount; i++)
        {
            // 2.将任务交给子物体
            childTF = tfGet(transform.GetChild(i), childName);
            if (childTF != null)
                return childTF;
        }
        return null;
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="transform"></param>
    /// <param name="objPath"></param>
    /// <returns></returns>
    public static T GetComponentInChildren<T>(this Transform transform, string objPath) where T : Component
    {
        Transform childTF = transform.Find(objPath);
        return childTF.GetComponent<T>() == null ? childTF.gameObject.AddComponent<T>() : childTF.GetComponent<T>();
    }

    #region 查找组件的拓展
    /// <summary>
    /// 未知层级,查找后代指定名称挂在的组件。
    /// </summary>
    /// <param name="currentTF">当前变换组件</param>
    /// <param name="childName">后代物体名称</param>
    /// <returns></returns>
    public static T OnFindAnyComponent<T>(this Transform transform, string childName) where T : Component
    {
        //递归:方法内部又调用自身的过程。
        //1.在子物体中查找
        return tfGet(transform, childName) != null ? tfGet(transform, childName).GetComponent<T>() : null;
    }

    /// <summary>
    /// 获取transform组件
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">需要获取的组件名字</param>
    /// <returns></returns>
    public static Transform OnGetTransform(this Transform transform, string name)
    {
        return transform.OnFindAnyComponent<Transform>(name);
    }

    /// <summary>
    /// 获取transform组件
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">需要获取的组件名字</param>
    /// <returns></returns>
    public static Text OnGetText(this Transform transform, string name)
    {
        return transform.OnFindAnyComponent<Text>(name);
    }

    /// <summary>
    /// 获取InputField组件
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">需要获取的组件名字</param>
    /// <returns></returns>
    public static InputField OnGetInputField(this Transform transform, string name)
    {
        return transform.OnFindAnyComponent<InputField>(name);
    }

    /// <summary>
    /// 获取InputField组件
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">需要获取的组件名字</param>
    /// <returns></returns>
    public static Toggle OnGetToggle(this Transform transform, string name)
    {
        return transform.OnFindAnyComponent<Toggle>(name);
    }

    /// <summary>
    /// 获取Image组件
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">需要获取的组件名字</param>
    /// <returns></returns>
    public static Image OnGetImage(this Transform transform, string name)
    {
        return transform.OnFindAnyComponent<Image>(name);
    }

    /// <summary>
    /// 获取SpriteRenderer组件
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">需要获取的组件名字</param>
    /// <returns></returns>
    public static SpriteRenderer OnGetSpriteRenderer(this Transform transform, string name)
    {
        return transform.OnFindAnyComponent<SpriteRenderer>(name);
    }

    /// <summary>
    /// 获取Dropdown组件
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">需要获取的组件名字</param>
    /// <returns></returns>
    public static Dropdown OnGetDropdown(this Transform transform, string name)
    {
        return transform.OnFindAnyComponent<Dropdown>(name);
    }

    /// <summary>
    /// 获取Button组件
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">需要获取的组件名字</param>
    /// <returns></returns>
    public static Button OnGetButton(this Transform transform, string name)
    {
        return transform.OnFindAnyComponent<Button>(name);
    }
    #endregion

    //*********************************组件清空子物体*********************************
    /// <summary>
    /// 清空子物体
    /// </summary>
    /// <param name="transform"></param>
    public static void ClearChild(this Transform transform)
    {
        for (int t = 0; t < transform?.childCount; t++)
            GameObject.Destroy(transform.GetChild(t).gameObject);
    }

    /// <summary>
    /// 清空子物体
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="number">需要跳过的数字</param>
    public static void ClearChild(this Transform transform, List<int> number)
    {
        for (int t = 0; t < transform?.childCount; t++)
        {
            bool isContinue = false;//是否跳过
            for (int n = 0; n < number?.Count; n++)
            {
                if (t == number[n]) { isContinue = true; continue; }
            }
            if (!isContinue)
            {
                GameObject.Destroy(transform.GetChild(t).gameObject);
            }
        }
    }
}

