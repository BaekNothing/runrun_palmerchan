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

    public override void Init()
    {
        InitGame();
        InitData();

        _initAction?.Invoke();

        IsReady = true;
    }

    void InitGame()
    {
        Application.targetFrameRate = 60;
        BindKey();
        BindMouse();
        BindPeriodicAction();
    }

    void InitData()
    {
        GameData.Instance.Init();
    }

    void BindKey()
    {
        InputManager.BindKey(KeyCode.Space, GameStart, ActionWrapper.ActionType.Released);
        InputManager.BindKey(KeyCode.Escape, GamePaused, ActionWrapper.ActionType.Released);
    }

    void GameStart()
    {
        if (GameData.Instance.State == GameData.GameState.Play)
            return;

        Time.timeScale = 1;

        GameData.Instance.Init();
        GameData.Instance.State = GameData.GameState.Play;
        _gameStartAction?.Invoke();
    }

    void GamePaused()
    {
        if (GameData.Instance.State == GameData.GameState.Pause)
            return;

        Time.timeScale = 0;

        GameData.Instance.State = GameData.GameState.Pause;
        _gamePausedAction?.Invoke();
    }

    void BindMouse()
    {
        InputManager.BindMouse(MouseButton.LeftMouse, GameStart, ActionWrapper.ActionType.Released);
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
    }

    public override void UpdateAction()
    {

    }
}
