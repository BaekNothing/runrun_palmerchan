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

    [Header("Initial Settings")]
    public float Mass = 1;
    public float GravityScale = 1;

    [Header("Game Settings")]
    public float Speed = 1;
    public float JumpForce = 1;
}
