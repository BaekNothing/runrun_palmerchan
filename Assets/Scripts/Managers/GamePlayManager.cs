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
    }

    void GameQuit()
    {
        Utility.Log("Game Quit");
    }

    void BindPeriodicAction()
    {
        PeriodicActionManager.BindPeriodicAction(Time.deltaTime, AddGameScore);
    }

    void AddGameScore()
    {
        GameData.Instance.Score += Time.deltaTime * GameData.Instance.Speed * 10;
    }

    public override void UpdateAction()
    {

    }
}