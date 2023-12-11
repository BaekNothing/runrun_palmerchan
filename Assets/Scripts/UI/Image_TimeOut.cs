using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_TimeOut : AbstractOnOffObject
{
    protected override void Init()
    {
        GamePlayManager.BindGameStartAction(StartGame);
        GamePlayManager.BindGameOverAction(EndGame);
        this.gameObject.SetActive(false);
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
