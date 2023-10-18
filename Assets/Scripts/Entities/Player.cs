using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    Rigidbody2D RB
    {
        get
        {
            if (rb == null)
                rb = GetComponent<Rigidbody2D>();
            return rb;
        }
    }

    void Start()
    {
        SetData();
        InputManager.BindKey(KeyCode.Space, Jump, InputManager.ActionType.Pressed);
    }

    void SetData()
    {
        RB.gravityScale = PlayerData.Instance.GravityScale;
        RB.mass = PlayerData.Instance.Mass;
    }

    void Jump()
    {
        RB.AddForce(Vector2.up * PlayerData.Instance.JumpForce, ForceMode2D.Impulse);
    }

}
