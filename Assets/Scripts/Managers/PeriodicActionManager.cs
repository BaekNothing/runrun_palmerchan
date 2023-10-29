using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PeriodicActionManager : AbstractManager
{
    [SerializeField] double _timer = 0;
    static readonly Dictionary<double, Action> _periodicAction = new();

    public override void Init()
    {
        IsReady = true;
    }

    public static void BindPeriodicAction(double time, Action action)
    {
        _periodicAction.Add(time, action);
    }

    public override void UpdateAction()
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
