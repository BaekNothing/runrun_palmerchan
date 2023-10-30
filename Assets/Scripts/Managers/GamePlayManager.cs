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
        // if this function binded keycode.space. it can make bug which is when player press space, game start and check supportitem at the same time.
        InputManager.BindKey(KeyCode.None, GameStart, ActionWrapper.ActionType.Pressed);
        InputManager.BindKey(KeyCode.Escape, GamePaused, ActionWrapper.ActionType.Released);
    }

    void GameStart()
    {
        if (GameData.Instance.State == GameData.GameState.Play)
            return;

        if (GameData.Instance.State != GameData.GameState.Pause)
            GameData.Instance.Init(); // it mean game first start

        GameData.Instance.State = GameData.GameState.Play;
        _gameStartAction?.Invoke();

        Time.timeScale = 1;
    }

    void GamePaused()
    {
        if (GameData.Instance.State == GameData.GameState.Pause)
            return;

        GameData.Instance.State = GameData.GameState.Pause;
        _gamePausedAction?.Invoke();

        Time.timeScale = 0;
    }

    void GameOver()
    {
        GameData.Instance.State = GameData.GameState.GameOver;
        _gameOverAction?.Invoke();
        Time.timeScale = 0;
    }

    void BindMouse()
    {
        // if this function binded ActionType.Pressed. it can make bug which is when player press space, game start and check supportitem at the same time.
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
