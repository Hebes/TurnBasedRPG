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
    #region �ɰ����
    //[MenuItem("Assets/����ProjectĿ¼")]
    //public static void Search()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    // ��ȡ����ѡ�� �ļ����ļ��е� GUID
    //    string[] guids = Selection.assetGUIDs;
    //    foreach (var guid in guids)
    //    {
    //        // �� GUID ת��Ϊ ·��
    //        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
    //        // �ж��Ƿ��ļ���
    //        if (Directory.Exists(assetPath))
    //        {
    //            SearchDirectory(sb, assetPath);
    //        }
    //    }
    //    Debug.Log(sb);
    //}

    //private static string _extension = "*.anim";
    //static void SearchDirectory(StringBuilder sb, string directory)
    //{
    //    DirectoryInfo dInfo = new DirectoryInfo(directory);
    //    // ��ȡ �ļ����Լ����ļ�����������չ��Ϊ  _extension ���ļ�
    //    FileInfo[] fileInfoArr = dInfo.GetFiles(_extension, SearchOption.AllDirectories);
    //    for (int i = 0; i < fileInfoArr.Length; ++i)
    //    {
    //        string fullName = fileInfoArr[i].FullName;
    //        sb.AppendLine(fullName);
    //    }
    //}
    #endregion

    private static string prefabLoadPath = "Prefabs";//ResourcesmĿ¼�µ�
    private static string ClassName = "ConfigUIPrefab";

    [MenuItem("Assets/������Ҫ���ص�����������ļ�")]
    public static void SearchPrefab()
    {
        //�������
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public static class {ClassName}");
        sb.AppendLine("{");
        sb.AppendLine("\t#region ��Ҫ���ص����������");
        Transform[] tf = Resources.LoadAll<Transform>(prefabLoadPath);
        if (tf.Length == 0) { Debug.Log("·������"); }
        foreach (var item in tf) { sb.AppendLine($"\tpublic static string {item.name} => \"{item.name}\";"); }
        sb.AppendLine("\t#endregion");
        sb.AppendLine("}");

        //�����ļ�
        string filePath = $"{Application.dataPath}/Core/Resource/PrefabMgr/{ClassName}.cs";
        if (File.Exists(filePath)) { File.Delete(filePath); }
        using (StreamWriter writer = File.CreateText(filePath)) { writer.Write(sb); Debug.Log("����д��ɹ�!"); }
        AssetDatabase.Refresh();
    }
}
