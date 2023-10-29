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

    private Action action;
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

    public static ActionWrapper operator +(ActionWrapper wrapper, Action action)
    {
        // prevent duplicate action
        wrapper.action -= action;
        wrapper.action += action;
        return wrapper;
    }

    public static ActionWrapper operator -(ActionWrapper wrapper, Action action)
    {
        wrapper.action -= action;
        return wrapper;
    }
}
