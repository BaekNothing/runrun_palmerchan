using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePlayManager : AbstractManager
{
    public override void Init()
    {
        InitGame();
        InitData();

        IsReady = true;
    }

    void InitGame()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 480, false);
        BindKey();
        BindPeriodicAction();
    }

    void InitData()
    {
        GameData.Instance.Init();
    }

    void BindKey()
    {
        InputManager.BindKey(KeyCode.Escape, GameQuit, ActionWrapper.ActionType.Released);
        InputManager.BindKey(KeyCode.Space, GameStart, ActionWrapper.ActionType.Released);
    }

    void GameStart()
    {
        if (GameData.Instance.State == GameData.GameState.Play)
            return;

        GameData.Instance.Init();
        GameData.Instance.State = GameData.GameState.Play;
    }

    void GameQuit()
    {
        Utility.Log("Game Quit");
    }

    void BindPeriodicAction()
    {
        PeriodicActionManager.BindPeriodicAction(PeriodicActionManager.EVERY_FRAME, AddGameScore);
    }

    void AddGameScore()
    {
        GameData.Instance.Score += Time.deltaTime * GameData.Instance.Speed * 10;
    }

    public override void UpdateAction()
    {

    }
}
