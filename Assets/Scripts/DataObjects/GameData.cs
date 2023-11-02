using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.IO;
using System;


#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor
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

        if (GUILayout.Button("SetScreenSize"))
        {
            // SetScreenSize in build setting
            var gameData = target as GameData;
            PlayerSettings.defaultWebScreenWidth = (int)gameData.ScreenSize.x;
            PlayerSettings.defaultWebScreenHeight = (int)gameData.ScreenSize.y;
        }

        base.OnInspectorGUI();
    }
    public void SaveDataInText()
    {
        var gameData = target as GameData;
        var fields = gameData.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        var text = "";
        foreach (var field in fields)
        {
            text += $"{field.Name} : {field.GetValue(gameData)}\n";
        }
        var path = gameData.DataPath.FullName + gameData.OptionsFileName;
        File.WriteAllText(path, text);
        AssetDatabase.Refresh();
    }

    public void LoadDataFromText()
    {
        var gameData = target as GameData;
        var path = gameData.DataPath.FullName + gameData.OptionsFileName;
        var text = File.ReadAllText(path);
        var lines = text.Split('\n');
        foreach (var line in lines)
        {
            var data = line.Split(':');
            if (data.Length != 2) continue;
            var fieldName = data[0].Trim();
            var fieldValue = data[1].Trim();
            var field = gameData.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                if (field.FieldType == typeof(int))
                {
                    field.SetValue(gameData, int.Parse(fieldValue));
                }
                else if (field.FieldType == typeof(float))
                {
                    field.SetValue(gameData, float.Parse(fieldValue));
                }
                else if (field.FieldType == typeof(double))
                {
                    field.SetValue(gameData, double.Parse(fieldValue));
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(gameData, fieldValue);
                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValue(gameData, bool.Parse(fieldValue));
                }
                else if (field.FieldType == typeof(Vector2))
                {
                    var vector2 = fieldValue.Split(',');
                    var x = float.Parse(vector2[0].Replace('(', ' ').Trim());
                    var y = float.Parse(vector2[1].Replace(')', ' ').Trim());
                    field.SetValue(gameData, new Vector2(x, y));
                }
            }
        }
    }
}

#endif


[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    static GameData instance;
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameData>("GameData");
            }
            return instance;
        }
    }

    public enum GameState
    {
        Ready = 0,
        Play,
        Pause,
        GameOver
    }

    [Header("Game Option")]
    public Utility.LogLevel LogLevel = Utility.LogLevel.Normal;
    public Vector2 ScreenSize = new(960, 250);
    public float orthographicSize = 5.0f;
    public DirectoryInfo DataPath = new(Application.dataPath + "/Resources/");
    public string OptionsFileName = "GameData.txt";
    public float PauseDealy = 0.5f;

    [Header("Game Object")]
    public Player PlayerPrefab;
    public BgObject BgObjectsPrefab;
    public ObstacleObject ObstaclePrefab;
    public SupportObject SupportObjectPrefab;

    [Header("Immutable Value")]
    public float Mass = 1;
    public float GravityScale = 4;

    public float CheckSupportObjectRadius = 0.5f;
    public float CheckSupportObjectDelay = 0.5f;

    public float TimeLimit = 30;

    public float SpeedMax = 10;
    public float SpeedMin = 3;
    public float SpeedIncreaseValue = 0.5f;
    public float SpeedDecreaseValue = 0.5f;
    public float PlayerPosX_L = -11f;
    public float PlayerPosX_R = -6f;
    public float PlayerPosY = 1f;
    public float PlayerPosZ = 0f;

    [Header("Mutable Value")]
    public float Speed = 1;
    public double Score = 0;
    public float Timer = 0;
    public GameState State = GameState.Ready;
    public DateTime PausedTime = DateTime.MinValue;

    public void SetSpeed(float speed) => Speed = speed;
    public void SetScore(double score) => Score = score;
    public void SetTimer(float timer) => Timer = timer;
    public void SetState(GameState state) => State = state;
    public void SetPausedTime(DateTime pausedTime) => PausedTime = pausedTime;

    public void Init()
    {
        Speed = SpeedMin;
        Score = 0;
        Timer = 0;
        State = GameState.Ready;
        PausedTime = DateTime.MinValue;
    }
}
