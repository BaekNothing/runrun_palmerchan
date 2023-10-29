using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWrapper
{
    public enum ActionType
    {
        None,
        Pressed,
        Released,
        Held
    }

    private readonly Action action;
    private readonly ActionType type;

    public ActionType Type => type;

    public ActionWrapper(Action action, ActionType type)
    {
        this.action = action;
        this.type = type;
    }

    public void Invoke(ActionType type)
    {
        if (type == ActionType.None)
            return;

        if (type == this.type)
        {
            action.Invoke();
        }
    }
}
