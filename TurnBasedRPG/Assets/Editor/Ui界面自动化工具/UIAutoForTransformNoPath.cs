using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ui组件自动化获取
/// </summary>
public class UIAutoForTransformNoPath : EditorWindow
{
    private const string TransformPrefix = "T_";//关键的前缀
    public string InputGameObjectTransform { get; private set; }//输入物体的Transform，就是前缀

    //[MenuItem("GameObject/组件查找和重命名(Shift+A) #A", false, 0)]
    //[MenuItem("Assets/组件查找和重命名(Shift+A) #A")]
    //[MenuItem("Tool/组件查找和重命名(Shift+A) #A", false, 0)]


    [MenuItem("Assets/UI组件获取工具/Transform组件查找-Transform没路径(Shift+A) #A")]
    public static void GeneratorFindComponentTool()
    {
        GetWindow(typeof(UIAutoForTransformNoPath), false, "Transform组件查找").Show();
    }

    private void OnGUI()
    {
        //******************************Transform组件查找打印 * *****************************配合Transform拓展
        GUILayout.BeginVertical("box");
        {
            EditorGUILayout.LabelField("Transform组件查找打印", EditorStyles.boldLabel);
            //******************************请输入Transform组件查找前缀******************************
            GUILayout.Space(5f); EditorGUILayout.LabelField("请输入组件查找前缀:", EditorStyles.largeLabel);
            if (GUILayout.Button("清空前缀", EditorStyles.miniButtonMid))
            {
                InputGameObjectTransform = string.Empty;
            }
            if (GUILayout.Button("常用前缀:transform", EditorStyles.miniButtonMid))
            {
                InputGameObjectTransform = "transform";
            }
            InputGameObjectTransform = GUILayout.TextField(InputGameObjectTransform, "BoldTextField");
            //******************************组件重命名******************************
            GUILayout.Space(5f); EditorGUILayout.LabelField($"组件重命名:", EditorStyles.largeLabel);
            if (GUILayout.Button($"前缀添加{TransformPrefix}", EditorStyles.miniButtonMid)) { AddPrefix(TransformPrefix); }
            if (GUILayout.Button($"去除前缀{TransformPrefix}", EditorStyles.miniButtonMid)) { RemovePrefix(TransformPrefix); }
            if (GUILayout.Button($"去除空白和特殊字符", EditorStyles.miniButtonMid)) { ClearTrim(); }
            if (GUILayout.Button($"保存修改", EditorStyles.miniButtonMid)) { SaveModification(); }
            if (GUILayout.Button($"删除物体(不可逆转,推荐官方删除)", EditorStyles.miniButtonMid))
            {
                if (EditorUtility.DisplayDialog("消息提示", "确定要删除吗", "确定", "取消"))
                {
                    DeleteGameObject();
                }
                else
                {

                }
            }
            //******************************Transform组件查找打印******************************
            GUILayout.Space(5f); EditorGUILayout.LabelField("获取属性或变量(二选一):", EditorStyles.largeLabel);
            if (GUILayout.Button("获取属性", EditorStyles.miniButtonMid))
            {
                GetComonpentProperty(new FindConfig() { KeyValue = TransformPrefix, isGetSet = true, });
            }
            if (GUILayout.Button("获取变量", EditorStyles.miniButtonMid))
            {
                GetComonpentProperty(new FindConfig() { KeyValue = TransformPrefix, isGetSet = false, });
            }
            //******************************获取组件******************************
            GUILayout.Space(5f); EditorGUILayout.LabelField("获取组件:", EditorStyles.largeLabel);
            if (GUILayout.Button("获取组件(赋值版)", EditorStyles.miniButtonMid))
            {
                GetComponentFind(new FindConfig()
                {
                    isAssign = true,
                    isAddPrefix = true,
                    KeyValue = TransformPrefix,
                    beginStr = InputGameObjectTransform,
                });
            }
            if (GUILayout.Button("获取组件(不赋值版)", EditorStyles.miniButtonMid))
            {
                GetComponentFind(new FindConfig()
                {
                    isAssign = false,
                    isAddPrefix = true,
                    KeyValue = TransformPrefix,
                    beginStr = InputGameObjectTransform,
                });
            }
            //******************************获取监听******************************
            GUILayout.Space(5f); EditorGUILayout.LabelField("按钮监听代码:", EditorStyles.largeLabel);
            if (GUILayout.Button("获取监听", EditorStyles.miniButtonMid))
            {
                GetComonpentListener(new FindConfig()
                {
                    KeyValue = TransformPrefix,
                    beginStr = InputGameObjectTransform,
                });
            }
            //******************************一键生成******************************
            GUILayout.Space(5f); EditorGUILayout.LabelField($"一键生成:", EditorStyles.largeLabel);
            if (GUILayout.Button($"一键获取(GetSet版本)", EditorStyles.miniButtonMid))
            {
                OneKeyGeneration(new FindConfig()
                {
                    isAssign = true,
                    beginStr = InputGameObjectTransform,
                    isAddPrefix = true,
                    isGetSet = true,
                    KeyValue = TransformPrefix,
                });
            }
            if (GUILayout.Button($"一键获取(变量版本)", EditorStyles.miniButtonMid))
            {
                OneKeyGeneration(new FindConfig()
                {
                    isAssign = true,
                    beginStr = InputGameObjectTransform,
                    isAddPrefix = true,
                    isGetSet = false,
                    KeyValue = TransformPrefix,
                });
            }
            //******************************获取选中的物体组件获取******************************
            GUILayout.Space(5f); EditorGUILayout.LabelField($"获取选中的物体组件获取:", EditorStyles.largeLabel);
            if (GUILayout.Button($"获取选中的物体组件获取", EditorStyles.miniButtonMid))
            {
                GetSelectGoCompent(new FindConfig());
            }
            //******************************一键去除组件RayCast Target******************************
            EditorGUILayout.LabelField("一键去除组件RayCast Target:", EditorStyles.largeLabel);
            if (GUILayout.Button("一键去除组件RayCast Target", EditorStyles.miniButtonMid)) { ClearRayCastTarget(); }
        }
        GUILayout.EndVertical(); GUILayout.Space(5f);
    }



    //******************************组合功能******************************
    #region 组合功能

    #region 组件重命名

    /// <summary>
    /// 添加前缀
    /// </summary>
    private void AddPrefix(string prefix)
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体
        foreach (var item in obj)
        {
            GameObject go = item as GameObject;

            if (go.name.StartsWith(prefix))
                continue;

            go.name = $"{prefix}{go.name}";
        }
    }

    /// <summary>
    /// 删除前缀
    /// </summary>
    private void RemovePrefix(string prefix)
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体 
        foreach (var item in obj)
        {
            GameObject go = item as GameObject;
            if (go.name.Contains(prefix))
                go.name = go.name.Replace(prefix, "");
        }
    }

    /// <summary>
    /// 去除空白
    /// </summary>
    private void ClearTrim()
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体 
        foreach (var item in obj)
            item.name = UIFindComponent.ClearSpecificSymbol(item.name);
    }

    /// <summary>
    /// 保存修改
    /// </summary>
    private void SaveModification()
    {
        Object[] obj = Selection.objects;


        for (int i = 0; i < obj.Length; i++)
        {
            Undo.RecordObject(obj[i], obj[i].name);
            EditorUtility.SetDirty(obj[i]);
        }
    }

    /// <summary>
    /// 删除物体
    /// </summary>
    private void DeleteGameObject()
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体
        foreach (GameObject item in obj)
            DestroyImmediate(item);
    }
    #endregion

    #region 组件属性

    /// <summary>
    /// 生成组件属性代码
    /// </summary>
    /// <param name="findtConfig">配置文件</param>
    private void GetComonpentProperty(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        findtConfig.controlDic = UIFindComponent.FindComponents1(obj, findtConfig.KeyValue);
        string str1 = ComonpentProperty(findtConfig);
        Debug.Log(str1.ToString());
        GUIUtility.systemCopyBuffer = str1.ToString();
    }

    #endregion

    #region 组件查找

    /// <summary>
    /// 生成组件查找代码
    /// </summary>
    private void GetComponentFind(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        findtConfig.controlDic = UIFindComponent.FindComponents1(obj, findtConfig.KeyValue);
        string str1 = ComponentFind(findtConfig);

        Debug.Log(str1.ToString());
        GUIUtility.systemCopyBuffer = str1.ToString();
    }
    #endregion

    #region 组件监听

    /// <summary>
    /// 生成监听代码
    /// </summary>
    private void GetComonpentListener(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        findtConfig.controlDic = UIFindComponent.FindComponents1(obj, findtConfig.KeyValue);
        string str1 = ComonpentListener(findtConfig);

        Debug.Log(str1.ToString());
        GUIUtility.systemCopyBuffer = str1.ToString();
    }
    #endregion

    #region 一键生成

    /// <summary>
    /// 一键生成
    /// </summary>
    /// <param name="KeyValue"></param>
    private void OneKeyGeneration(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents1(obj, findtConfig.KeyValue);
        findtConfig.controlDic = ComponentsDic;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region UI代码 自动化生成");
        //组件属性
        sb.AppendLine(ComonpentProperty(findtConfig));
        //组件获取
        sb.AppendLine(ComponentFind(findtConfig));
        //组件监听
        sb.AppendLine(ComonpentListener(findtConfig));
        sb.AppendLine("#endregion");
        Debug.Log(sb.ToString());
        GUIUtility.systemCopyBuffer = sb.ToString();
    }
    #endregion

    #region 获取选中的物体的组件

    /// <summary>
    /// 获取选中的物体的组件
    /// </summary>
    /// <param name="KeyValue"></param>
    private void GetSelectGoCompent(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj1 = Selection.objects.First() as GameObject;
        List<Component> components = new List<Component>();
        List<string> vs = new List<string>()
        {
            typeof(Image).Name,
            typeof(Transform).Name,
            typeof(Button).Name ,
            typeof(CanvasGroup).Name ,
            typeof(Text).Name ,
            //请后续自行添加
        };
        vs.ForEach((str) =>
        {
            Component Temp = obj1.transform.GetComponent(str);
            if (Temp != null)
                components.Add(Temp);
        });
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region 自动化获取代码组件");
        //属性代码
        foreach (var item in components)
        {
            string temp = TypeChange(item.GetType().Name);
            sb.AppendLine($"public {temp} {item.gameObject.name}{temp} {{ set; get; }}");
        }
        sb.AppendLine();
        //获取组件代码
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// 获取选中物体的组件");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("private void OnGetSelectComponent()");
        sb.AppendLine("{");
        foreach (var item in components)
        {
            string temp = TypeChange(item.GetType().Name);
            sb.AppendLine($"\t{item.gameObject.name}{temp} = GetComponent<{temp}>();");
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        Debug.Log(sb);
        GUIUtility.systemCopyBuffer = sb.ToString();
    }
    #endregion

    #region 去除组件RayCast Target

    /// <summary>
    /// 去除组件RayCast Target
    /// </summary>
    private void ClearRayCastTarget()
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体
        foreach (var item in obj)
        {
            GameObject go = item as GameObject;
            if (go.GetComponent<Text>() != null)
            {
                go.GetComponent<Text>().raycastTarget = false;
                continue;
            }
            else if (go.GetComponent<Image>())
            {
                go.GetComponent<Image>().raycastTarget = false;
                continue;
            }
            else if (go.GetComponent<RawImage>())
            {
                go.GetComponent<RawImage>().raycastTarget = false;
                continue;
            }
            if (EditorUtility.DisplayDialog("消息提示", go.name + "没有找到需要去除的RayCast Target选项", "确定")) { }
        }
    }
    #endregion

    #endregion

    //******************************基础功能******************************
    #region 基础功能

    /// <summary>
    /// 组件属性
    /// </summary>
    /// <param name="obj"></param>
    public static string ComonpentProperty(FindConfig findtConfig)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region 组件模块 不要的代码请自行删除");
        foreach (string Key in findtConfig.controlDic?.Keys)
        {
            string type = TypeChange(Key);//类型
            findtConfig.controlDic[Key]?.ForEach((component) =>
            {
                string componentName = UIFindComponent.ClearSpecificSymbol(component.name);//组件名称,順便出去空白
                if (findtConfig.isGetSet)
                    sb.AppendLine($"public {type} {componentName}{type} {{ set; get; }}");
                else
                    sb.AppendLine($"public {type} {componentName}{type};");
            });
            sb.AppendLine();
        }
        sb.AppendLine("#endregion");
        return sb.ToString();
    }

    /// <summary>
    /// 组件查找
    /// </summary>
    /// <param name="beginStr"></param>
    /// <param name="controlDic"></param>
    public static string ComponentFind(FindConfig findtConfig)
    {
        //添加前缀
        string beginStr = findtConfig.isAddPrefix ? AddPrefix1(findtConfig.beginStr) : string.Empty;
        //打印
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region 获取组件模块");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// 获取组件");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("private void OnGetComponent()");
        sb.AppendLine("{");
        foreach (string Key in findtConfig.controlDic?.Keys)
        {
            string type = TypeChange(Key);//类型
            findtConfig.controlDic[Key]?.ForEach((component) =>
            {
                string componentName = UIFindComponent.ClearSpecificSymbol(component.name);//组件名称,順便出去空白
                if (findtConfig.isAssign)
                    sb.AppendLine($"\t{componentName}{type} = {beginStr}OnGet{type}(\"{componentName}\");");
                else
                    sb.AppendLine($"\t{beginStr}OnGet{type}(\"{componentName}\");");
            });
            sb.AppendLine();
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");

        return sb.ToString();
    }


    /// <summary>
    /// 组件监听
    /// </summary>
    /// <param name="beginStr"></param>
    /// <param name="controlDic"></param>
    public static string ComonpentListener(FindConfig findtConfig)
    {
        //添加前缀
        //string beginStr = findtConfig.isAddPrefix ? AddPrefix1(findtConfig.beginStr) : string.Empty;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region 按钮监听模块");
        sb.AppendLine("/// <summary>"); sb.AppendLine("/// 按钮监听"); sb.AppendLine("/// </summary>");
        sb.AppendLine("private void OnAddListener()");
        sb.AppendLine("{");
        foreach (string Key in findtConfig.controlDic?.Keys)
        {
            switch (Key)
            {
                case "Button":
                    //V_HeiShiButton.onClick.AddListener(cityUI.HeiShi); 模板
                    findtConfig.controlDic[Key]?.ForEach((component) =>
                    {
                        string componentName = UIFindComponent.ClearSpecificSymbol(component.name);//组件名称,順便出去空白
                        sb.AppendLine($"\t{componentName}{Key}.onClick.AddListener({componentName}AddListener);");
                    });
                    sb.AppendLine();
                    break;
                case "Toggle":
                    //toggle.onValueChanged.AddListener(toggleAddListener); 模板
                    findtConfig.controlDic[Key]?.ForEach((component) =>
                    {
                        string componentName = UIFindComponent.ClearSpecificSymbol(component.name);//组件名称,順便出去空白
                        sb.AppendLine($"\t{componentName}{Key}.onValueChanged.AddListener({componentName}AddListener);");
                    });
                    sb.AppendLine();
                    break;
                default:
                    break;
            }
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        return sb.ToString();
    }

    #endregion

    //******************************其他******************************
    #region 其他

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="typeStr"></param>
    /// <returns></returns>
    private static string TypeChange(string typeStr)
    {
        switch (typeStr)
        {
            case "RectTransform": return "Transform";
            default: return typeStr;
        }
    }
    /// <summary>
    /// 添加前缀
    /// </summary>
    /// <returns></returns>
    private static string AddPrefix1(string beginStr)
    {
        return !string.IsNullOrEmpty(beginStr) ? $"{beginStr}." : beginStr;//添加前缀
    }

    #endregion
}
