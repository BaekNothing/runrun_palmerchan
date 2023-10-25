using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public double timer = 0;

    static Dictionary<double, Action> _periodicAction = new();

    public static void BindPeriodicAction(double time, Action action)
    {
        _periodicAction.Add(time, action);
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 480, false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        foreach (var action in _periodicAction)
        {
            if (0 <= timer % action.Key && timer % action.Key < Time.deltaTime)
            {
                action.Value.Invoke();
            }
        }
    }



}
