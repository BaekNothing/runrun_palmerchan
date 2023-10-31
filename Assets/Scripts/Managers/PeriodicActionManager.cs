using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PeriodicActionManager : AbstractManager
{
    [SerializeField] double _timer = 0;
    static readonly Dictionary<double, Action> _periodicAction = new();

    public const double EVERY_FRAME = -1;

    public override void Init()
    {
        IsReady = true;
    }

    public static void BindPeriodicAction(double time, Action action)
    {
        if (_periodicAction.ContainsKey(time))
        {
            // prevent duplicate action
            _periodicAction[time] -= action;
            _periodicAction[time] += action;
        }
        else
        {
            _periodicAction.Add(time, action);
        }
    }

    public void UpdateAction()
    {
        _timer += Time.deltaTime;

        foreach (var action in _periodicAction)
        {
            if (action.Key <= EVERY_FRAME)
            {
                action.Value.Invoke();
            }
            else if (0 <= _timer % action.Key && _timer % action.Key < Time.deltaTime)
            {
                action.Value.Invoke();
            }
        }
    }
}
