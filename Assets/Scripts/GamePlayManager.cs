using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] double _timer = 0;
    static readonly Dictionary<double, Action> _periodicAction = new();

    public static void BindPeriodicAction(double time, Action action)
    {
        _periodicAction.Add(time, action);
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 480, false);
        PlayerData.Instance.Init();
    }

    void Update()
    {
        _timer += Time.deltaTime;

        foreach (var action in _periodicAction)
        {
            if (0 <= _timer % action.Key && _timer % action.Key < Time.deltaTime)
            {
                action.Value.Invoke();
            }
        }
    }



}
