using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputManager : AbstractManager
{
    [SerializeField] KeyCode _pressedKey = KeyCode.None;

    public override void Init()
    {
        IsReady = true;
    }

    static readonly Dictionary<KeyCode, ActionWrapper> keyBindings = new();

    public static void BindKey(KeyCode key, Action action, ActionWrapper.ActionType type)
    {
        keyBindings.Add(key, new ActionWrapper(action, type));
    }

    public static void UnbindKey(KeyCode key)
    {
        keyBindings.Remove(key);
    }

    public override void UpdateAction()
    {
        foreach (var key in keyBindings.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                keyBindings[key].Invoke(ActionWrapper.ActionType.Pressed);
                _pressedKey = key;
            }
            else if (Input.GetKeyUp(key))
            {
                keyBindings[key].Invoke(ActionWrapper.ActionType.Released);
                _pressedKey = KeyCode.None;
            }
            else if (Input.GetKey(key))
            {
                keyBindings[key].Invoke(ActionWrapper.ActionType.Held);
                _pressedKey = key;
            }
        }
    }
}
