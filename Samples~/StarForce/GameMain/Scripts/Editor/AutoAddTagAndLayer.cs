using UnityEditor;

public class AutoAddTagAndLayer
{

    //private static string[] LayerArr = { "LayerOne", "LayerTwo", "LayerThree" };
    //private static string[] TagArr = { "TagOne", "TagTwo", "TagThree" };

    [InitializeOnLoadMethod]
    private static void OnEditorLaunch()
    {
        //UnityEngine.Debug.Log("OnEditorLaunch");
        //代码重新编译时该方法也会重新执行
        //使用时间判断避免重复执行
        if (EditorApplication.timeSinceStartup < 30)
        {
            //UnityEngine.Debug.Log("OnEditorLaunch EditorApplication.timeSinceStartup < 30");
            //向此委托添加函数，以便将其执行延迟到检视面板更新完成之后
            //每个函数在添加后仅执行一次
            EditorApplication.delayCall += () =>
            {
                //UnityEngine.Debug.Log("OnEditorLaunch  delayCall");
                //foreach (string tag in TagArr)
                //{
                //    AddTag(tag);
                //}
                //foreach (string layer in LayerArr)
                //{
                //    AddLayer(layer);
                //}
                AddLayer(StarForce.Constant.Layer.TargetableObjectLayerName);
            };
        }
    }

    private static void AddTag(string tag)
    {
        if (!IsHasTag(tag))
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "tags")
                {
                    it.InsertArrayElementAtIndex(it.arraySize);
                    SerializedProperty dataPoint = it.GetArrayElementAtIndex(it.arraySize - 1);
                    dataPoint.stringValue = tag;
                    tagManager.ApplyModifiedProperties();
                    return;
                }
            }
        }
    }

    private static void AddLayer(string layer)
    {
        if (!IsHasLayer(layer))
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "layers")
                {
                    for (int i = 0; i < it.arraySize; i++)
                    {
                        if (i == 3 || i == 6 || i == 7)
                        {
                            continue;
                        }
                        SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                        if (string.IsNullOrEmpty(dataPoint.stringValue))
                        {
                            dataPoint.stringValue = layer;
                            tagManager.ApplyModifiedProperties();
                            return;
                        }
                    }
                }
            }
        }
    }

    private static bool IsHasTag(string tag)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(tag))
                return true;
        }
        return false;
    }

    private static bool IsHasLayer(string layer)
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/Tagmanager.asset"));
        SerializedProperty it = tagManager.GetIterator();
        while (it.NextVisible(true))
        {
            if (it.name == "layers")
            {
                for (int i = 0; i < it.arraySize; i++)
                {
                    SerializedProperty sp = it.GetArrayElementAtIndex(i);
                    if (!string.IsNullOrEmpty(sp.stringValue))
                    {
                        if (sp.stringValue.Equals(layer))
                        {
                            sp.stringValue = string.Empty;
                            tagManager.ApplyModifiedProperties();
                        }
                    }
                }
            }
        }
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.layers[i].Contains(layer))
                return true;
        }
        return false;
    }
}