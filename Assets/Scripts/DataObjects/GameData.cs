using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {

        if (GUILayout.Button("SetScreenSize"))
        {
            // SetScreenSize in build setting
            var gameData = target as GameData;
            PlayerSettings.defaultWebScreenWidth = (int)gameData.ScreenSize.x;
            PlayerSettings.defaultWebScreenHeight = (int)gameData.ScreenSize.y;
        }

        base.OnInspectorGUI();
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

    public float SpeedMax = 10;
    public float SpeedMin = 3;
    public float SpeedIncreaseValue = 0.5f;

    [Header("Mutable Value")]
    public float Speed = 1;
    public double Score = 0;
    public GameState State = GameState.Ready;

    public void Init()
    {
        Speed = SpeedMin;
        Score = 0;
        State = GameState.Ready;
    }
}
