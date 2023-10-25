using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(PlayerData))]
public class PlayerDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Apply"))
        {
            var player = FindObjectsByType<Player>(FindObjectsSortMode.InstanceID);
            if (player == null)
            {
                Debug.LogError("Player is not found");
                return;
            }
            else if (player.Length > 1)
            {
                Debug.LogError("There are more than one player");
                return;
            }
            else
                player[0].SetData();
        }
    }
}


#endif


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    static PlayerData instance;
    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<PlayerData>("PlayerData");
                Debug.Log(instance);
            }
            return instance;
        }
    }

    public void Init()
    {
        Speed = SpeedMin;
    }

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
}
