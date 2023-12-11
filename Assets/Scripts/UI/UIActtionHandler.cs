using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameCoreSystem))]
public class UIActtionHandler : MonoBehaviour
{
    static Action _invokePause;
    public static void BindPauseAction(Action action)
    {
        // prevent duplicate action
        _invokePause -= action;
        _invokePause += action;
    }

    public void InvokePause()
    {
        _invokePause?.Invoke();
    }

    public void InvokeCredits()
    {
        _invokePause?.Invoke();
        //Move to github repo
        Application.OpenURL("https://github.com/BaekNothing/runrun_palmerchan");
    }
}
