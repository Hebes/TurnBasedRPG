using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LogUtils;
using UnityEngine;

public class DataMgr : BaseManager<DataMgr>
{
    public DataMgr()
    {
        DataLoadAndAnalysis();
    }

    /// <summary>技能数据</summary>
    public List<SkillsInfo> SkillLists { get; private set; }
    /// <summary>怪物信息</summary>
    public List<EnemyInfo> enemyInfo { get; private set; }
    /// <summary>场景配置表</summary>
    public List<SceneInfo> sceneInfo { get; private set; }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enemyInfo"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public T GetData<T>(List<T> infos, int id) where T : CSVBaseInfo
    {
        return infos.Find((info) => { return info.GetID() == id; });
    }

    /// <summary>
    /// 数据加载和解析
    /// </summary>
    private void DataLoadAndAnalysis()
    {
        SkillLists= CSVAnalysis<SkillsInfo>(ResModule.Instance.Load<TextAsset>("CSV/技能"));
        enemyInfo = CSVAnalysis<EnemyInfo>(ResModule.Instance.Load<TextAsset>("CSV/怪物"));//Assets/Resources/CSV/物理.csv
        sceneInfo = CSVAnalysis<SceneInfo>(ResModule.Instance.Load<TextAsset>("CSV/场景配置表"));//Assets/Resources/CSV/物理.csv
    }

    /// <summary>
    /// CSV文件解析
    /// </summary>
    /// <param name="Path">文件路径</param>
    public List<T> CSVAnalysis<T>(TextAsset textAsset, int StartRow = 6) where T : CSVBaseInfo, new()
    {
        List<T> Temp = new List<T>();
        string[] textAssetText = CSVReadContent(textAsset);
        for (int i = StartRow; i < textAssetText.Length; i++)
        {
            if (string.IsNullOrEmpty(textAssetText[i])) continue;
            Temp.Add(CSVOneDataAnalysis<T>(textAssetText[i]));
        }
        return Temp;
    }

    /// <summary>
    /// 读取内容
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    private string[] CSVReadContent(TextAsset textAsset)
    {
        string[] textAssetText = textAsset.text.Split("\n");
        return textAssetText;
    }

    /// <summary>
    /// 一条数据解析
    /// </summary>
    private T CSVOneDataAnalysis<T>(string textAssetText) where T : new()
    {
        List<string> count = RedOneRow(textAssetText);
        T charect = new T();
        Type type = charect.GetType();
        PropertyInfo[] fieldInfos = type.GetProperties();
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            PropertyInfo item = fieldInfos[i];
            item.SetValue(charect, CSVDataChange(count[i], item.PropertyType));
        }
        return charect;
    }

    /// <summary>
    /// 读取单行
    /// </summary>
    /// <param name="textAssetText">单行内容</param>
    private static List<string> RedOneRow(string textAssetText)
    {
        List<string> content = new List<string>();
        string tempStr = textAssetText.Replace("\r", "");
        content.AddRange(tempStr.Split(','));
        return content;
    }

    /// <summary>
    /// 反射数据类型并赋值
    /// </summary>
    /// <param name="str">内容</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    private static object CSVDataChange(string str, Type type)
    {
        if (type == typeof(int)) { return int.Parse(str); }
        else if (type == typeof(float)) { return float.Parse(str); }
        else if (type == typeof(string)) { return str; }
        else if (type == typeof(bool)) { return str.ToLower() == "ture" ? true : false; }
        else if (type == typeof(List<string>)) { return str.Split('|').ToList(); }
        else if (type == typeof(List<int>))
        {
            List<string> vs = str.Split('|').ToList();
            return vs.Select<string, int>(a => Convert.ToInt32(a)).ToList(); ;
        }
        else if (type == typeof(List<float>))
        {
            List<string> vs = str.Split('|').ToList();
            return vs.Select<string, float>(a => Convert.ToSingle(a)).ToList(); ;
        }
        else if (type == typeof(Sprite))
        {
            return null;
        }
        else
        {
            Debug.Log(string.Format($"<color=#FFFF00>{type.Name} 未知类型,请添加解析类型!</color>"));
            return null;
        }
    }
}
