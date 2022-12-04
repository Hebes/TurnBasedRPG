using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �ο����ӣ�https://blog.csdn.net/liqiangeastsun/article/details/42124283
/// https://blog.csdn.net/LIQIANGEASTSUN/article/details/42173941
/// </summary>
public class AssetsProject : MonoBehaviour
{
    private static string _extension = "*.anim";

    [MenuItem("Assets/����ProjectĿ¼")]
    public static void Search()
    {
        StringBuilder sb = new StringBuilder();
        // ��ȡ����ѡ�� �ļ����ļ��е� GUID
        string[] guids = Selection.assetGUIDs;
        foreach (var guid in guids)
        {
            // �� GUID ת��Ϊ ·��
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // �ж��Ƿ��ļ���
            if (Directory.Exists(assetPath))
            {
                SearchDirectory(sb, assetPath);
            }
        }
        Debug.Log(sb);
    }

    [MenuItem("Assets/���������Ҫ���ص����������")]
    public static void SearchPrefab()
    {
        StringBuilder sb = new StringBuilder();
        Transform[] tf = Resources.LoadAll<Transform>("Prefabs");//ResourcesmĿ¼�µ�
        foreach (var item in tf)
        {
            sb.AppendLine($"public static string {item.name} => \"{item.name}\";");
        }
        Debug.Log(sb);
    }


    static void SearchDirectory(StringBuilder sb, string directory)
    {
        DirectoryInfo dInfo = new DirectoryInfo(directory);
        // ��ȡ �ļ����Լ����ļ�����������չ��Ϊ  _extension ���ļ�
        FileInfo[] fileInfoArr = dInfo.GetFiles(_extension, SearchOption.AllDirectories);
        for (int i = 0; i < fileInfoArr.Length; ++i)
        {
            string fullName = fileInfoArr[i].FullName;
            sb.AppendLine(fullName);
        }
    }
}
