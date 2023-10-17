using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float Speed = 1;
    public float Mass = 1;
    public float JumpForce = 1;
    public float GravityScale = 1;
}
