using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] EffectController _effectController;
    public EffectController EffectController
    {
        get
        {
            if (_effectController == null)
                _effectController = GetComponent<EffectController>();
            return _effectController;
        }
    }

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

    enum PlayerCheckEffectState
    {
        MISS = 0,
        GREAT,
        PERFECT
    }

    void Start()
    {
        SetData();
        BindKey();
        BindPeriodicAction();
    }

    public void SetData()
    {
        RB.gravityScale = GameData.Instance.GravityScale;
        RB.mass = GameData.Instance.Mass;
    }

    void BindKey()
    {
        InputManager.BindKey(KeyCode.Space, CheckSupportObject, ActionWrapper.ActionType.Pressed);
    }

    void CheckSupportObject()
    {
        if (_checkSupportDelay < GameData.Instance.CheckSupportObjectDelay) return;
        _checkSupportDelay = 0;

        if (Physics2D.OverlapCircle(transform.position,
            GameData.Instance.CheckSupportObjectRadius, LayerMask.GetMask("SupportObjects")))
        {
            // if player check support object, reset check delay
            _checkSupportDelay = GameData.Instance.CheckSupportObjectDelay;

            EffectController.PlayEffect((int)PlayerCheckEffectState.PERFECT);
            GameData.Instance.Speed += GameData.Instance.SpeedIncreaseValue;
            Utility.Log("Speed: " + GameData.Instance.Speed, Utility.LogLevel.Verbose);
        }
        else
        {
            EffectController.PlayEffect((int)PlayerCheckEffectState.MISS);
        }
    }

    void BindPeriodicAction()
    {
        PeriodicActionManager.BindPeriodicAction(Time.deltaTime, IncreaseCheckSupportDelay);
    }

    void IncreaseCheckSupportDelay()
    {
        _checkSupportDelay += Time.deltaTime;
    }

}
