using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_StartTitle : AbstractOnOffObject
{
    protected override void Init()
    {
        GamePlayManager.BindGameStartAction(StartGame);
        GamePlayManager.BindGamePausedAction(EndGame);
    }

    void StartGame()
    {
        this.gameObject.SetActive(false);
    }

    void EndGame()
    {
        this.gameObject.SetActive(true);
    }
}
