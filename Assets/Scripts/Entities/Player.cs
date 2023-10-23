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

    [SerializeField] Animator ani;
    Animator Ani
    {
        get
        {
            if (ani == null)
                ani = GetComponent<Animator>();
            return ani;
        }
    }

    void Start()
    {
        SetData();
        BindKeys();
    }

    public void SetData()
    {
        RB.gravityScale = PlayerData.Instance.GravityScale;
        RB.mass = PlayerData.Instance.Mass;
    }

    void BindKeys()
    {
        InputManager.BindKey(KeyCode.Space, Jump, InputManager.ActionType.Released);
        Debug.Log("Jump");
    }

    void Jump()
    {
        RB.AddForce(Vector2.up * PlayerData.Instance.JumpForce, ForceMode2D.Impulse);
        ani.SetTrigger("Jump");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ani.SetTrigger("Land");
        }
    }
}
