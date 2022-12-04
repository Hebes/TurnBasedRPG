using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 参考连接：https://blog.csdn.net/liqiangeastsun/article/details/42124283
/// https://blog.csdn.net/LIQIANGEASTSUN/article/details/42173941
/// </summary>
public class AssetsProject : MonoBehaviour
{
    private static string _extension = "*.anim";

    [MenuItem("Assets/测试Project目录")]
    public static void Search()
    {
        StringBuilder sb = new StringBuilder();
        // 获取所有选中 文件、文件夹的 GUID
        string[] guids = Selection.assetGUIDs;
        foreach (var guid in guids)
        {
            // 将 GUID 转换为 路径
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // 判断是否文件夹
            if (Directory.Exists(assetPath))
            {
                SearchDirectory(sb, assetPath);
            }
        }
        Debug.Log(sb);
    }

    [MenuItem("Assets/输出所有需要加载的物体的名称")]
    public static void SearchPrefab()
    {
        StringBuilder sb = new StringBuilder();
        Transform[] tf = Resources.LoadAll<Transform>("Prefabs");//Resourcesm目录下的
        foreach (var item in tf)
        {
            sb.AppendLine($"public static string {item.name} => \"{item.name}\";");
        }
        Debug.Log(sb);
    }


    static void SearchDirectory(StringBuilder sb, string directory)
    {
        DirectoryInfo dInfo = new DirectoryInfo(directory);
        // 获取 文件夹以及子文件加中所有扩展名为  _extension 的文件
        FileInfo[] fileInfoArr = dInfo.GetFiles(_extension, SearchOption.AllDirectories);
        for (int i = 0; i < fileInfoArr.Length; ++i)
        {
            string fullName = fileInfoArr[i].FullName;
            sb.AppendLine(fullName);
        }
    }
}
