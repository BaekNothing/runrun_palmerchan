using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerObjects", menuName = "ScriptableObjects/PlayerObjects", order = 1)]
public class PlayerObjects : ScriptableObject
{
    static PlayerObjects instance;
    public static PlayerObjects Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<PlayerObjects>("PlayerObjects");
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
