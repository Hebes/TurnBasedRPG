using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI组件查找
/// </summary>
public class UIFindComponent : Editor
{
    //******************************通用代码******************************
    #region 通用代码

    public static Dictionary<string, List<Component>> FindComponents1(GameObject obj, string keyValue)
    {
        Dictionary<string, List<Component>> controlDic = new Dictionary<string, List<Component>>();

        //查找组件
        FindChildrenControl1<Button>(obj, controlDic, keyValue);
        FindChildrenControl1<Image>(obj, controlDic, keyValue);
        FindChildrenControl1<Text>(obj, controlDic, keyValue);
        FindChildrenControl1<Toggle>(obj, controlDic, keyValue);
        FindChildrenControl1<Slider>(obj, controlDic, keyValue);
        FindChildrenControl1<ScrollRect>(obj, controlDic, keyValue);
        FindChildrenControl1<InputField>(obj, controlDic, keyValue);
        FindChildrenControl1<Transform>(obj, controlDic, keyValue);
        FindChildrenControl1<ToggleGroup>(obj, controlDic, keyValue);
        FindChildrenControl1<Dropdown>(obj, controlDic, keyValue);
        return controlDic;
    }

    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private static void FindChildrenControl1<T>(GameObject gameObject, Dictionary<string, List<Component>> controlDic, string keyValue) where T : Component
    {
        T[] controls = gameObject.GetComponentsInChildren<T>();
        string objType = controls.Length == 0 ? string.Empty : controls[0].GetType().Name;//获取组件类型字符串
        for (int i = 0; i < controls?.Length; i++)
        {
            string objName = controls[i].name;//获取组件的名称
            if (!objName.StartsWith(keyValue)) //keyValue = V_
                continue;
            if (controlDic.ContainsKey(objType))//字典里面有这个组件
                controlDic[objType].Add(controls[i]);
            else
                controlDic.Add(objType, new List<Component>() { controls[i] });
        }
    }



    /// <summary>
    /// 字符串去除特殊符号空白等
    /// </summary>
    public static string ClearSpecificSymbol(string str)
    {
        return str.Replace(" ", "").Replace("(", "").Replace(")", "").Trim();//组件名称,順便出去空白
    }



    #endregion

    //******************************杂项代码******************************
    #region 杂项代码

    /// <summary>
    /// 文件以追加写入的方式
    /// https://wenku.baidu.com/view/a8fdb767fd4733687e21af45b307e87100f6f85b.html
    /// 显示IO异常请在创建文件的时候Close下
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="content">内容</param>
    private static void FileWriteContent(string path, string content)
    {
        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(content);
        using (FileStream fsWrite = new FileStream(path, FileMode.Append, FileAccess.Write))
        {
            fsWrite.Write(myByte, 0, myByte.Length);
        }
    }

    /// <summary>
    /// 通过路径检文件夹是否存在，如果不存在则创建
    /// </summary>
    /// <param name="folderPath"></param>
    private static void ChackFolder(string folderPath)
    {
        if (!Directory.Exists(folderPath))//是否存在这个文件
        {
            Debug.Log("文件夹不存在,正在创建...");
            Directory.CreateDirectory(folderPath);//创建
            AssetDatabase.Refresh();//刷新编辑器
            Debug.Log("创建成功!");
        }
    }

    /// <summary>
    /// 添加前缀
    /// </summary>
    /// <returns></returns>
    private static string AddPrefix(string beginStr)
    {
        //添加前缀
        if (!string.IsNullOrEmpty(beginStr))
            return $"{beginStr}.";
        return beginStr;
    }

    /// <summary>
    /// 生成文件并写入内容
    /// </summary>
    /// <param name="folderPath">文件夹路径</param>
    /// <param name="fileName">文件名</param>
    /// <param name="content">内容</param>
    public static void CreatCSharpScript(string folderPath, string fileName, string content)
    {
        //创建并写入内容
        string filePath = $"{folderPath}/{fileName}";
        if (!File.Exists(filePath))
        {
            Debug.Log("文件不存在,进行创建...");
            using (StreamWriter writer = File.CreateText(filePath))//生成文件
            {
                writer.Write(content);
                Debug.Log("内容写入成功!");
            }
        }
        //刷新unity编辑器
        AssetDatabase.Refresh();
    }

    #endregion
}

/// <summary>
/// Find类型查找配置文件
/// </summary>
public class FindConfig
{
    public FindConfig()
    {
        controlDic = new Dictionary<string, List<Component>>();
    }

    /// <summary>
    /// 关键词 例如V_
    /// </summary>

    public string KeyValue { get; set; }

    /// <summary>
    /// 选中的物体名称
    /// </summary>
    public string selectGoName { get; set; }

    /// <summary>
    /// 是否添加前缀
    /// </summary>
    public bool isAddPrefix { get; set; } = false;

    /// <summary>
    /// 是否赋值
    /// </summary>
    public bool isAssign { get; set; }

    /// <summary>
    /// 是否是GetSet生成
    /// </summary>
    public bool isGetSet { get; set; } = true;

    /// <summary>
    /// 用于代码生成输入的前缀
    /// </summary>
    public string beginStr { get; set; }

    /// <summary>
    /// 数据字典
    /// </summary>
    public Dictionary<string, List<Component>> controlDic { get; set; }
}
