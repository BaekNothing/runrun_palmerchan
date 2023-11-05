using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

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
    public void SetSupportDelay(float delay) => _checkSupportDelay = delay;

    [SerializeField] Vector3 _positionBySpeed = Vector3.zero;

    enum PlayerCheckEffectState
    {
        MISS = 0,
        GREAT,
        PERFECT
    }

    void Awake()
    {
        SetData();
        BindGameAction();
        BindKey();
        BindPeriodicAction();
    }

    void BindGameAction()
    {
        GamePlayManager.BindGameStartAction(() => SetSupportDelay(GameData.Instance.CheckSupportObjectDelay / 2));
    }

    public void SetData()
    {
        RB.gravityScale = GameData.Instance.GravityScale;
        RB.mass = GameData.Instance.Mass;
        _positionBySpeed.x = GameData.Instance.PlayerPosX_L;
        _positionBySpeed.y = GameData.Instance.PlayerPosY;
        _positionBySpeed.z = GameData.Instance.PlayerPosZ;

        EffectController.BindCutInIsOn(IsCutInActive);
    }

    bool IsCutInActive(int index)
    {
        if (index == (int)PlayerCheckEffectState.PERFECT)
            return true;
        else
            return false;
    }

    void BindKey()
    {
        InputManager.BindKey(KeyCode.Space, CheckSupportObject, ActionWrapper.ActionType.Pressed);
        InputManager.BindMouse(MouseButton.LeftMouse, CheckSupportObject, ActionWrapper.ActionType.Pressed);
    }

    void CheckSupportObject()
    {
        if (GameData.Instance.State != GameData.GameState.Play)
            return;

        if (Utility.CheckGameIsPaused())
            return;

        if (_checkSupportDelay < GameData.Instance.CheckSupportObjectDelay)
            return;
        SetSupportDelay(0);

        var supportObject = Physics2D.OverlapCircle(transform.position,
            GameData.Instance.CheckSupportObjectRadius * (GameData.Instance.Speed - GameData.Instance.SpeedMin) * 0.5f
            , LayerMask.GetMask("SupportObjects"));

        if (supportObject)
        {
            // if player check support object, reset check delay
            SetSupportDelay(GameData.Instance.CheckSupportObjectDelay);

            var supportType = supportObject.GetComponent<SupportObject>()?.GetSupportType();
            if (supportType == SupportObject.SupportType.Common)
            {
                EffectController.PlayEffect((int)PlayerCheckEffectState.GREAT);
            }
            else
            {
                EffectController.PlayEffect((int)PlayerCheckEffectState.PERFECT);
            }

            GameData.Instance.SetSpeed(
                Mathf.Min( // speed can't be over than speed max
                    GameData.Instance.SpeedIncreaseValue + GameData.Instance.Speed,
                    GameData.Instance.SpeedMax));
            Utility.Log("Speed: " + GameData.Instance.Speed, Utility.LogLevel.Verbose);
        }
        else
        {
            EffectController.PlayEffect((int)PlayerCheckEffectState.MISS);
            GameData.Instance.SetSpeed(
                Mathf.Max( // speed can't be lower than speed min
                    GameData.Instance.Speed - GameData.Instance.SpeedDecreaseValue,
                    GameData.Instance.SpeedMin));
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
