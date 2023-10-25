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

    [SerializeField] float _checkSupportDelay = 0;

    void Start()
    {
        SetData();
        BindKey();
        BindPeriodicAction();
    }

    public void SetData()
    {
        RB.gravityScale = PlayerData.Instance.GravityScale;
        RB.mass = PlayerData.Instance.Mass;
    }

    void BindKey()
    {
        InputManager.BindKey(KeyCode.Space, CheckSupportObject, InputManager.ActionType.Pressed);
        Debug.Log("Jump");
    }

    void CheckSupportObject()
    {
        if (_checkSupportDelay < PlayerData.Instance.CheckSupportObjectDelay) return;
        _checkSupportDelay = 0;

        if (Physics2D.OverlapCircle(transform.position, PlayerData.Instance.CheckSupportObjectRadius, LayerMask.GetMask("SupportObjects")))
        {
            PlayerData.Instance.Speed += PlayerData.Instance.SupportObjectValue;
            Debug.Log("Speed Up");
        }
    }

    void BindPeriodicAction()
    {
        GamePlayManager.BindPeriodicAction(Time.deltaTime, IncreaseCheckSupportDelay);
    }

    void IncreaseCheckSupportDelay()
    {
        _checkSupportDelay += Time.deltaTime;
    }

}
