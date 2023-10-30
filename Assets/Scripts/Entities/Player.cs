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
    [SerializeField] Vector3 _positionBySpeed = Vector3.zero;

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
        _positionBySpeed.x = GameData.Instance.PlayerPosX_L;
        _positionBySpeed.y = GameData.Instance.PlayerPosY;
        _positionBySpeed.z = GameData.Instance.PlayerPosZ;
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
            GameData.Instance.Speed = Mathf.Min(GameData.Instance.Speed, GameData.Instance.SpeedMax);

            Utility.Log("Speed: " + GameData.Instance.Speed, Utility.LogLevel.Verbose);
        }
        else
        {
            EffectController.PlayEffect((int)PlayerCheckEffectState.MISS);
            GameData.Instance.Speed -= GameData.Instance.SpeedDecreaseValue;
            GameData.Instance.Speed = Mathf.Max(GameData.Instance.Speed, GameData.Instance.SpeedMin);
        }
    }

    void BindPeriodicAction()
    {
        PeriodicActionManager.BindPeriodicAction(PeriodicActionManager.EVERY_FRAME, IncreaseCheckSupportDelay);
        PeriodicActionManager.BindPeriodicAction(PeriodicActionManager.EVERY_FRAME, SetPositionBySpeed);
    }

    void IncreaseCheckSupportDelay()
    {
        _checkSupportDelay += Time.deltaTime;
    }

    //
    void SetPositionBySpeed()
    {
        _positionBySpeed.x = Mathf.Lerp(
            GameData.Instance.PlayerPosX_L,
            GameData.Instance.PlayerPosX_R,
            (GameData.Instance.Speed - GameData.Instance.SpeedMin) /
            (GameData.Instance.SpeedMax - GameData.Instance.SpeedMin));

        // move smoothly
        transform.position = Vector3.Lerp(
            transform.position,
            _positionBySpeed,
            Time.deltaTime * 10 * GameData.Instance.CheckSupportObjectDelay);
    }
}
