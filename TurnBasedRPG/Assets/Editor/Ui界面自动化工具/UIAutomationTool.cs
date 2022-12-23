
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIAutomationTool : EditorWindow
{
    private Vector2 scrollPosition { get; set; } = Vector2.zero;

    private const string UIprefix = "V_";
    private const string TransformPrefix = "T_";
    private static bool isAddPrefix { get; set; }
    public string InputComponentName { get; private set; }
    public string InputTransformComponentName { get; private set; }
    public string TransformGetPrefix { get; private set; }

    /// <summary>是否赋值</summary>
    public bool isAssign { get; private set; }
    /// <summary>
    /// 是否复制到剪切板-tf直接获取的版本
    /// </summary>
    public bool isCopyBoard3 { get; private set; }
    

    [MenuItem("GameObject/组件查找和重命名(Shift+A) #A", false, 0)]
    [MenuItem("Assets/组件查找和重命名(Shift+A) #A")]
    [MenuItem("Tool/组件查找和重命名(Shift+A) #A", false, 0)]
    public static void GeneratorFindComponentTool() => EditorWindow.GetWindow(typeof(UIAutomationTool), false, "组件查找和重命名(Shift+A)").Show();

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);//GUILayout.Width(400), GUILayout.Height(500)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(position.width), GUILayout.Height(position.height));
            {
                GUILayout.Space(5f);

                //******************************UI组件查找打印******************************
                GUILayout.BeginVertical("box", GUILayout.Width(200f));//垂直
                {
                    EditorGUILayout.LabelField("Debug自动生成的代码的前缀", EditorStyles.label);
                    //******************************请输入前缀******************************
                    GUILayout.Space(5f);
                    GUILayout.Label("请输入前缀:", GUILayout.Width(70f));
                    if (GUILayout.Button("清空前缀", GUILayout.Width(200f)))
                    {
                        InputComponentName = string.Empty;
                    }
                    InputComponentName = GUILayout.TextField(InputComponentName, "BoldTextField", GUILayout.Width(200f));
                    //******************************生成Config脚本******************************
                    GUILayout.Space(5f);
                    //EditorGUILayout.LabelField("生成Config脚本", EditorStyles.label);
                    //if (GUILayout.Button("生成Config脚本", GUILayout.Width(150))) { CreatConfig(); }
                    EditorGUILayout.LabelField("Debug生成Config代码", GUILayout.Width(200f));//EditorStyles.label
                    if (GUILayout.Button("打印生成Config代码", GUILayout.Width(200f)))
                    {
                        PrintConfig(new FindConfig()
                        {
                            KeyValue = UIprefix,
                        });
                    }
                    //******************************组件查找代码******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("组件查找代码", GUILayout.Width(100f));
                    isAddPrefix = GUILayout.Toggle(isAddPrefix, "是否添加前缀");
                    if (GUILayout.Button("组件查找代码", GUILayout.Width(200)))
                    {
                        ComponentFind(new FindConfig()
                        {
                            isAddPrefix = isAddPrefix,
                            KeyValue = UIprefix,
                            beginStr = InputComponentName,
                            findComponentType = FindConfig.FindComponentType.UIFind,
                        });
                    }
                    //******************************按钮监听代码******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("按钮监听代码", GUILayout.Width(100f));
                    if (GUILayout.Button("按钮监听代码", GUILayout.Width(200)))
                    {
                        AddListener(new FindConfig()
                        {
                            KeyValue = UIprefix,
                            beginStr = InputComponentName,
                        });
                    }
                    //******************************组件重命名******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"前缀添加{UIprefix}:", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"前缀添加{UIprefix}", GUILayout.Width(200))) { AddPrefix(UIprefix); }
                    if (GUILayout.Button($"去除前缀{UIprefix}", GUILayout.Width(200))) { RemovePrefix(UIprefix); }
                    //******************************保存修改******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"保存修改", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"保存修改", GUILayout.Width(200))) { SaveModification(); }
                    //******************************一键生成******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"一键生成", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"一键生成", GUILayout.Width(200)))
                    {
                        OneKeyGeneration(new FindConfig()
                        {
                            beginStr = InputComponentName,
                            findComponentType = FindConfig.FindComponentType.UIFind,
                            isAddPrefix = isAddPrefix,
                            isGetSet = true,
                            KeyValue = UIprefix,
                        });
                    }
                }
                GUILayout.EndVertical(); GUILayout.Space(5f);

                //******************************Transform组件查找打印******************************配合Transform拓展
                GUILayout.BeginVertical("box", GUILayout.Width(200f));
                {
                    EditorGUILayout.LabelField("Transform组件查找打印", EditorStyles.label);
                    //******************************请输入Transform组件查找前缀******************************
                    GUILayout.Space(5f);
                    GUILayout.Label("请输入Transform组件查找前缀:", GUILayout.Width(200f));
                    if (GUILayout.Button("清空前缀", GUILayout.Width(200f)))
                    {
                        InputTransformComponentName = string.Empty;
                    }
                    InputTransformComponentName = GUILayout.TextField(InputTransformComponentName, "BoldTextField", GUILayout.Width(200f));
                    //******************************Transform组件查找打印******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("Transform组件查找打印:", GUILayout.Width(170f));//EditorStyles.label
                    if (GUILayout.Button("Transform组件查找打印", GUILayout.Width(200)))
                    {
                        PrintConfig(new FindConfig()
                        {
                            KeyValue = TransformPrefix,
                        });
                    }
                    //******************************Transform组件获取打印******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("Transform组件获取打印:", GUILayout.Width(170f));//EditorStyles.label
                    isAssign = GUILayout.Toggle(isAssign, "是否使用不赋值版本代码");
                    if (GUILayout.Button("Transform组件获取打印", GUILayout.Width(200)))
                    {
                        //如果是不赋值版本的话
                        if (isAssign)
                            ComponentFind_DontAssign(new FindConfig()
                            {
                                isAddPrefix = true,
                                KeyValue = TransformPrefix,
                                beginStr = InputTransformComponentName,
                                findComponentType = FindConfig.FindComponentType.TfFing
                            });
                        else
                            ComponentFind(new FindConfig()
                            {
                                isAddPrefix = true,
                                KeyValue = TransformPrefix,
                                beginStr = InputTransformComponentName,
                                findComponentType = FindConfig.FindComponentType.TfFing
                            });
                    }
                    //******************************按钮监听代码******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("按钮监听代码", GUILayout.Width(100f));
                    isAddPrefix = GUILayout.Toggle(isAddPrefix, "是否添加前缀");
                    if (GUILayout.Button("按钮监听代码", GUILayout.Width(200)))
                    {
                        AddListener(new FindConfig()
                        {
                            KeyValue = TransformPrefix,
                            beginStr = InputTransformComponentName,
                            isAddPrefix = isAddPrefix,
                        });
                    }
                    //******************************组件重命名******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"前缀添加{TransformPrefix}:", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"前缀添加{TransformPrefix}", GUILayout.Width(200))) { AddPrefix(TransformPrefix); }
                    if (GUILayout.Button($"去除前缀{TransformPrefix}", GUILayout.Width(200))) { RemovePrefix(TransformPrefix); }
                    //******************************保存修改******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"保存修改", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"保存修改", GUILayout.Width(200))) { SaveModification(); }
                    //******************************一键生成******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"一键生成", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"一键生成", GUILayout.Width(200)))
                    {
                        OneKeyGeneration(new FindConfig()
                        {
                            beginStr = InputTransformComponentName,
                            findComponentType = FindConfig.FindComponentType.TfFing,
                            isAddPrefix = true,
                            isGetSet = true,
                            KeyValue = TransformPrefix,
                        });
                    }
                    //******************************获取选中的物体组件获取******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"获取选中的物体组件获取", GUILayout.Width(200f));//EditorStyles.label
                    if (GUILayout.Button($"获取选中的物体组件获取", GUILayout.Width(200)))
                    {
                        GetSelectGoCompent(new FindConfig());
                    }
                }
                GUILayout.EndVertical(); GUILayout.Space(5f);

                //******************************Transform组件直接查找打印******************************可以直接使用
                GUILayout.BeginVertical("box", GUILayout.Width(200f));
                {
                    EditorGUILayout.LabelField("Transform组件直接查找打印", EditorStyles.label);
                    //******************************请输入Transform组件直接查找前缀******************************
                    GUILayout.Space(5f);
                    GUILayout.Label("请输入Transform组件查找前缀:", GUILayout.Width(200f));
                    if (GUILayout.Button("清空前缀", GUILayout.Width(200f)))
                    {
                        TransformGetPrefix = string.Empty;
                    }
                    TransformGetPrefix = GUILayout.TextField(TransformGetPrefix, "BoldTextField", GUILayout.Width(200f));
                    //******************************Transform组件直接获取打印******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("Transform组件直接获取打印:", GUILayout.Width(170f));//EditorStyles.label
                    if (GUILayout.Button("Transform组件直接获取打印", GUILayout.Width(200)))
                    {
                        DirectGetTransformComonpentFind();
                    }
                    //******************************Transform组件直接获取打印******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("一键获取组件直接查找:", GUILayout.Width(170f));//EditorStyles.label
                    if (GUILayout.Button("一键获取组件直接查找", GUILayout.Width(200)))
                    {
                        DirectGetTransformComonpent();
                    }
                    //******************************提示框******************************
                    if (isCopyBoard3)
                        EditorGUILayout.HelpBox("已经复制到剪切板", MessageType.Info, true);
                }
                GUILayout.EndVertical(); GUILayout.Space(5f);
            }
            EditorGUILayout.EndHorizontal(); GUILayout.Space(5f);
        }
        GUILayout.EndScrollView();

        //******************************一键去除组件RayCast Target******************************
        GUILayout.BeginVertical("box");
        {
            EditorGUILayout.LabelField("一键去除组件RayCast Target", GUILayout.Width(200f));
            EditorGUILayout.LabelField("一键去除组件RayCast Target:", GUILayout.Width(170f));//EditorStyles.label
            if (GUILayout.Button("一键去除组件RayCast Target", GUILayout.Width(200))) { ClearRayCastTarget(); }
        }
        GUILayout.EndVertical(); GUILayout.Space(5f);
    }

    /// <summary>
    /// 即使刷新页面函数 OnSelectionChange
    /// </summary>
    private void OnSelectionChange() => Repaint();

    //******************************方法******************************

    /// <summary>
    /// 直接获取组件
    /// </summary>
    private void DirectGetTransformComonpent()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> controlDic = UIFindComponent.FindComponents(obj, TransformPrefix);
        Dictionary<string, List<Component>> controlPathDic = new Dictionary<string, List<Component>>();
        //数据转换
        foreach (var item in controlDic.Values)
        {
            for (int i = 0; i < item.Count; i++)
            {

                //临时变量
                List<string> strs = new List<string>();
                Component component = item[i];
                string strs1 = component.GetType().Name;
                Transform transformTF = item[i].transform;
                strs.Add(transformTF.name);
                string path = string.Empty;
                //获取路径
                while (transformTF.parent != null)
                {
                    transformTF = transformTF.parent;
                    if (transformTF.name == obj.name) break;
                    strs.Add(transformTF.name);
                }
                //转换成路径
                for (int j = strs.Count - 1; j >= 0; j--)
                {
                    //if (j != 0)
                    //    path += $"{strs[j]}/";
                    //else
                    //    path += $"{strs[j]}";
                    path += j != 0 ? $"{strs[j]}/" : $"{strs[j]}";
                }
                //添加到字典
                if (!controlPathDic.ContainsKey(path))
                    controlPathDic.Add(path, new List<Component>() { component });
                else
                    controlPathDic[path].Add(component);
            }
        }
        //生成组件属性
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region UI代码 自动化生成");
        sb.AppendLine("#region 组件属性");
        foreach (var item in controlPathDic.Values)
        {
            item.ForEach((component) =>
            {
                string componentName = component.name;
                string type = component.GetType().Name;
                switch (component.name)
                {
                    case "RectTransform": componentName = "Transform"; break;
                }
                sb.AppendLine($"public {type} {componentName}{type} {{ set; get; }}");
            });
        }
        sb.AppendLine("#endregion");
        //生成组件获取代码
        sb.AppendLine().AppendLine();
        sb.AppendLine("#region 组件通过路径获取");
        sb.AppendLine("private void OnPathGetComponent()");
        sb.AppendLine("{");
        foreach (var item in controlPathDic)
        {
            item.Value.ForEach((component) =>
            {
                string componentName = component.name;
                string type = component.GetType().Name;
                switch (type)
                {
                    case "RectTransform": type = "Transform"; break;
                }
                sb.AppendLine($"\t{componentName}{type} = {TransformGetPrefix}.GetComponentInChildren<{type}>(\"{item.Key}\");");
            });
        }
        sb.AppendLine("#endregion");
        sb.AppendLine("}");
        sb.AppendLine().AppendLine();
        //按钮代码监听
        sb.AppendLine("#region 按钮监听");
        sb.AppendLine("private void OnAddListener()");
        sb.AppendLine("{");
        foreach (var item in controlPathDic)
        {
            item.Value.ForEach((component) =>
            {
                string componentName = component.name;
                string type = component.GetType().Name;

                switch (type)
                {
                    case "Button":
                        sb.AppendLine($"\t{componentName}{type}.onClick.AddListener({componentName}AddListener));");
                        break;
                    case "Toggle":
                        sb.AppendLine($"\t{componentName}{type}.onValueChanged.AddListener({componentName}AddListener)));");
                        break;
                }
            });
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        //Debug.Log(sb);
        //显示提示信息
        CopyWord(sb.ToString());
        isCopyBoard3 = true;
        CloseBoard();
    }

    /// <summary>
    /// 直接获取组件--生成组件获取代码
    /// </summary>
    private void DirectGetTransformComonpentFind()
    {
        StringBuilder sb = new StringBuilder();
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> controlDic = UIFindComponent.FindComponents(obj, TransformPrefix);
        Dictionary<string, List<Component>> controlPathDic = new Dictionary<string, List<Component>>();
        //数据转换
        foreach (var item in controlDic.Values)
        {
            for (int i = 0; i < item.Count; i++)
            {
                //临时变量
                List<string> strs = new List<string>();
                Component component = item[i];
                string strs1 = component.GetType().Name;
                Transform transformTF = item[i].transform;
                strs.Add(transformTF.name);
                string path = string.Empty;
                //获取路径
                while (transformTF.parent != null)
                {
                    transformTF = transformTF.parent;
                    if (transformTF.name == obj.name) break;
                    strs.Add(transformTF.name);
                }
                //转换成路径
                for (int j = strs.Count - 1; j >= 0; j--)
                {
                    //if (j != 0)
                    //    path += $"{strs[j]}/";
                    //else
                    //    path += $"{strs[j]}";
                    path += j != 0 ? $"{strs[j]}/" : $"{strs[j]}";
                }
                //添加到字典
                if (!controlPathDic.ContainsKey(path))
                    controlPathDic.Add(path, new List<Component>() { component });
                else
                    controlPathDic[path].Add(component);
            }
        }
        //生成组件获取代码
        foreach (var item in controlPathDic)
        {
            item.Value.ForEach((component) =>
            {
                string componentName = component.name;
                string type = component.GetType().Name;
                switch (type)
                {
                    case "RectTransform": type = "Transform"; break;
                }
                sb.AppendLine($"{TransformGetPrefix}.GetComponentInChildren<{type}>(\"{item.Key}\");");
            });
        }
        Debug.Log(sb.ToString());
        //显示提示信息
        CopyWord(sb.ToString());
        isCopyBoard3 = true;
        CloseBoard();
    }
    /// <summary>
    /// 一键生成
    /// </summary>
    /// <param name="KeyValue"></param>
    private void OneKeyGeneration(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents(obj, findtConfig.KeyValue);
        findtConfig.controlDic = ComponentsDic;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region UI代码 自动化生成");
        //打印组件代码
        sb.AppendLine(UIFindComponent.DebugOutDemo(findtConfig));
        //获取组件
        sb.AppendLine(UIFindComponent.DebugOutGetComponentDemo(findtConfig));
        //按钮监听
        sb.AppendLine(UIFindComponent.DebugOutAddListenerDemo(findtConfig));
        sb.AppendLine("#endregion");
        Debug.Log(sb.ToString());
        GUIUtility.systemCopyBuffer = sb.ToString();
    }

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
        foreach (var item in components)
        {
            string temp = item.GetType().Name;
            switch (temp)
            {
                case "RectTransform":
                    temp = "Transform";
                    break;
            }
            sb.AppendLine($"public {temp} {item.gameObject.name}{temp} {{ set; get; }}");
        }
        sb.AppendLine();
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// 获取选中物体的组件");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("private void OnGetSelectComponent()");
        sb.AppendLine("{");
        foreach (var item in components)
        {
            string temp = item.GetType().Name;
            switch (temp)
            {
                case "RectTransform":
                    temp = "Transform";
                    break;
            }
            sb.AppendLine($"\t{item.gameObject.name}{temp} = GetComponent<{temp}>();");
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        //Debug.Log(sb);
        GUIUtility.systemCopyBuffer = sb.ToString();
    }

    ///// <summary>
    ///// 生成Config脚本
    ///// </summary>
    //private void CreatConfig()
    //{
    //    //获取到当前选择的物体
    //    GameObject obj = Selection.objects.First() as GameObject;
    //    Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents(obj, UIprefix);
    //    UIFindComponent.CreatCSharpScript(obj, ComponentsDic);
    //}

    /// <summary>
    /// 打印生成Config代码
    /// </summary>
    private void PrintConfig(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        findtConfig.controlDic = UIFindComponent.FindComponents(obj, findtConfig.KeyValue);
        UIFindComponent.DebugOutDemo(findtConfig);
    }

    /// <summary>
    /// 打印组件查找代码
    /// </summary>
    private void ComponentFind(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        findtConfig.controlDic = UIFindComponent.FindComponents(obj, findtConfig.KeyValue);
        UIFindComponent.DebugOutGetComponentDemo(findtConfig);//getComponent.
    }

    /// <summary>
    /// 打印组件查找代码-不赋值版本-使用地方:直接获取物体组件时使用
    /// </summary>
    private void ComponentFind_DontAssign(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        findtConfig.controlDic = UIFindComponent.FindComponents(obj, findtConfig.KeyValue);
        UIFindComponent.DebugOutGetComponentDemo_DontAssign(findtConfig);//getComponent.
    }

    /// <summary>
    /// 监听代码
    /// </summary>
    private void AddListener(FindConfig findtConfig)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        findtConfig.controlDic = UIFindComponent.FindComponents(obj, findtConfig.KeyValue);
        UIFindComponent.DebugOutAddListenerDemo(findtConfig);
    }

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

    //*****************************************************其他*****************************************************
    /// <summary>
    /// 保存修改
    /// </summary>
    private void SaveModification()
    {
        Object[] obj = Selection.objects;
        for (int i = 0; i < obj.Length; i++)
        {
            Undo.RecordObject(obj[i], "modify test value");
            EditorUtility.SetDirty(obj[i]);
        }
    }
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
                //if (EditorUtility.DisplayDialog("消息提示", "已去除:" + go.name + "的RayCast Target选项", "确定")) { }
                continue;
            }
            else if (go.GetComponent<Image>())
            {
                go.GetComponent<Image>().raycastTarget = false;
                //if (EditorUtility.DisplayDialog("消息提示", "已去除:" + go.name + "的RayCast Target选项", "确定")) { }
                continue;
            }
            else if (go.GetComponent<RawImage>())
            {
                go.GetComponent<RawImage>().raycastTarget = false;
                //if (EditorUtility.DisplayDialog("消息提示", "已去除:" + go.name + "的RayCast Target选项", "确定")) { }
                continue;
            }
            if (EditorUtility.DisplayDialog("消息提示", go.name + "没有找到需要去除的RayCast Target选项", "确定")) { }
        }
    }

    /// <summary>
    /// 将信息复制到剪切板当中 https://blog.csdn.net/LLLLL__/article/details/114463650
    /// </summary>
    public void CopyWord(string str)
    {
        TextEditor te = new TextEditor();
        te.text = str;
        te.SelectAll();
        te.Copy();
    }

    /// <summary>
    /// 关闭提示版
    /// </summary>
    private async void CloseBoard()
    {
        await Task.Delay(500);
        isCopyBoard3 = false;
    }
}
