using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.IO;
using System;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(ObjectData))]
public class ObjectDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("SaveDataInText"))
        {
            SaveDataInText();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("LoadDataFromText"))
        {
            LoadDataFromText();
        }

        GUILayout.Space(10);

        base.OnInspectorGUI();
    }

    public void SaveDataInText()
    {
        var objectData = target as ObjectData;
        var fields = objectData.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        var text = "";
        foreach (var field in fields)
        {
            text += $"{field.Name} : {field.GetValue(objectData)}\n";
        }
        var path = objectData.DataPath.FullName + objectData.OptionsFileName;
        File.WriteAllText(path, text);
        AssetDatabase.Refresh();
    }

    public void LoadDataFromText()
    {
        var objectData = target as ObjectData;
        var path = objectData.DataPath.FullName + objectData.OptionsFileName;
        var text = File.ReadAllText(path);
        var lines = text.Split('\n');
        foreach (var line in lines)
        {
            var data = line.Split(':');
            if (data.Length != 2) continue;
            var fieldName = data[0].Trim();
            var fieldValue = data[1].Trim();
            var field = objectData.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                if (field.FieldType == typeof(int))
                {
                    field.SetValue(objectData, int.Parse(fieldValue));
                }
                else if (field.FieldType == typeof(float))
                {
                    field.SetValue(objectData, float.Parse(fieldValue));
                }
                else if (field.FieldType == typeof(double))
                {
                    field.SetValue(objectData, double.Parse(fieldValue));
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(objectData, fieldValue);
                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValue(objectData, bool.Parse(fieldValue));
                }
                else if (field.FieldType == typeof(Vector2))
                {
                    var vector2 = fieldValue.Split(',');
                    var x = float.Parse(vector2[0].Replace('(', ' ').Trim());
                    var y = float.Parse(vector2[1].Replace(')', ' ').Trim());
                    field.SetValue(objectData, new Vector2(x, y));
                }
            }
        }
    }
}
#endif

[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectData", order = 1)]
public class ObjectData : ScriptableObject
{
    static ObjectData instance;
    public static ObjectData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<ObjectData>("ObjectData");
            }
            return instance;
        }
    }

    [Header("Game Option")]
    public DirectoryInfo DataPath = new(Application.dataPath + "/Resources/");
    public string OptionsFileName = "ObjectData.txt";

    [Header("Game Object")]
    public Player PlayerPrefab;
    public BgObject BgObjectsPrefab;
    public ObstacleObject ObstaclePrefab;
    public SupportObject SupportObjectPrefab;

    [Header("Effect Object")]
    public PoolObject TouchEffectPrefab;
    public int TouchEffectPoolSize = 1;
    public PoolObject TouchTailPrefab;
    public int TouchTailPoolSize = 1;

    [Header("Immutable Value")]
    public float SupportProbability = 0.95f; // this is condition of create supportObject (0.1 = 10%)
    public float DashBaseSpeed = 5f; // below this speed, dashObject can't create
    public float DashProbability = 0.1f; // this is condition of create dashObject (0.1 = 10%)

    public Vector3 ResetPosition = new(20, 0, 0);

    public int BgObjectMaxCount = 10;
    public Vector2 BgObjectRandomRange = new(0, 110);
    public Vector2 BgObjectDepthRange = new(0.2f, 1.0f);

    public Vector2 SupportRandomRange = new(0, 0);
    public float SupportObjectDepth = 1f;
    public int SupportObjectMaxCount = 3;
    public float SupportObjectCreateDelay = 0.3f;

    public bool CheckSupportObject()
    {
        return UnityEngine.Random.Range(0f, 1f) <= SupportProbability;
    }

    public bool CheckDashObject()
    {
        return GameData.Instance.Speed >= DashBaseSpeed &&
            UnityEngine.Random.Range(0f, 1f) <= DashProbability;
    }
}
