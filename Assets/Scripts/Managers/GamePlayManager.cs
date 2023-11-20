using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class GamePlayManager : AbstractManager
{
    static Action _initAction;
    public static void BindInitAction(Action action)
    {
        // prevent duplicate action
        _initAction -= action;
        _initAction += action;
    }
    static Action _gameStartAction;
    public static void BindGameStartAction(Action action)
    {
        // prevent duplicate action
        _gameStartAction -= action;
        _gameStartAction += action;
    }
    static Action _gamePausedAction;
    public static void BindGamePausedAction(Action action)
    {
        // prevent duplicate action
        _gamePausedAction -= action;
        _gamePausedAction += action;
    }
    static Action _gameOverAction;
    public static void BindGameOverAction(Action action)
    {
        // prevent duplicate action
        _gameOverAction -= action;
        _gameOverAction += action;
    }

    [SerializeField] Camera _mainCamera;
    public Camera MainCamera
    {
        get
        {
            if (_mainCamera == null)
                _mainCamera = Camera.main;
            return _mainCamera;
        }
    }


    public override void Init()
    {
        InitGame();
        InitData();
        InitUIAction();

        _initAction?.Invoke();

        IsReady = true;
    }

    void InitGame()
    {
        Application.targetFrameRate = 60;
        float currentRatio = (float)Screen.height / (float)Screen.width;
        float ratio = (GameData.Instance.ScreenSize.y / GameData.Instance.ScreenSize.x);

        // ratio : orthographicSize = currentRatio : x
        // x = orthographicSize * currentRatio / ratio

        // keep show same horizontal size
        MainCamera.orthographicSize = GameData.Instance.orthographicSize * currentRatio / ratio;

        BindKey();
        BindMouse();
        BindPeriodicAction();
    }

    void BindKey()
    {
        // if this function binded keycode.space. it can make bug which is when player press space, game start and check supportitem at the same time.
        InputManager.BindKey(KeyCode.None, GameStart, ActionWrapper.ActionType.Pressed);
        InputManager.BindKey(KeyCode.Escape, GamePaused, ActionWrapper.ActionType.Pressed);
    }

    void GameStart()
    {
        if (GameData.Instance.State == GameData.GameState.Play)
            return;

        if (GameData.Instance.State != GameData.GameState.Pause)
            GameData.Instance.Init(); // it mean game first start

        _gameStartAction?.Invoke();
        Utility.ResumeGame();

        GameData.Instance.State = GameData.GameState.Play;
    }

    public void GamePaused()
    {
        if (GameData.Instance.State == GameData.GameState.Pause)
            return;

        GameData.Instance.State = GameData.GameState.Pause;
        _gamePausedAction?.Invoke();

        Utility.PauseGame();
    }

    void BindMouse()
    {
        // if this function binded ActionType.Pressed. it can make bug which is when player press space, game start and check supportitem at the same time.
        InputManager.BindMouse(MouseButton.LeftMouse, GameStart, ActionWrapper.ActionType.Released);
        InputManager.BindMouse(MouseButton.LeftMouse, ShowTouchEffect, ActionWrapper.ActionType.Pressed);
        InputManager.BindMouse(MouseButton.LeftMouse, ShowTouchTailEffect, ActionWrapper.ActionType.Held);
        InputManager.BindMouse(MouseButton.LeftMouse, HideTouchTailEffect, ActionWrapper.ActionType.Released);
    }

    void ShowTouchEffect()
    {
        Vector3 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var touchEffect = ObjectPoolSystem.GetObject(ObjectPoolSystem.PoolType.TouchEffect);
        if (touchEffect != null)
            touchEffect.transform.position = mousePosition;

    }

    PoolObject _touchTail;

    void ShowTouchTailEffect()
    {
        if (_touchTail == null)
            _touchTail = ObjectPoolSystem.GetObject(ObjectPoolSystem.PoolType.TouchTail);

        if (_touchTail != null)
        {
            Vector3 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            _touchTail.transform.position = mousePosition;
        }
    }

    void HideTouchTailEffect()
    {
        if (_touchTail == null)
            return;

        ObjectPoolSystem.ReturnObject(ObjectPoolSystem.PoolType.TouchTail, _touchTail);
        _touchTail = null;
    }

    void BindPeriodicAction()
    {
        PeriodicActionManager.BindPeriodicAction(PeriodicActionManager.EVERY_FRAME, AddGameScore);
        PeriodicActionManager.BindPeriodicAction(PeriodicActionManager.EVERY_FRAME, AddTimer);
    }

    void AddGameScore()
    {
        GameData.Instance.Score += Time.deltaTime * GameData.Instance.Speed * 10;
    }

    void AddTimer()
    {
        GameData.Instance.Timer += Time.deltaTime;

        if (GameData.Instance.State == GameData.GameState.Play &&
            GameData.Instance.Timer > GameData.Instance.TimeLimit)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameData.Instance.State = GameData.GameState.GameOver;
        _gameOverAction?.Invoke();

        Utility.PauseGame();
    }

    void InitData()
    {
        GameData.Instance.Init();
    }

    void InitUIAction()
    {
        UIActtionHandler.BindPauseAction(GamePaused);
    }
}
