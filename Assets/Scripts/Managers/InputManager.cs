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

    static readonly Dictionary<KeyCode, List<ActionWrapper>> keyBindings = new();

    public static void BindKey(KeyCode key, Action action, ActionWrapper.ActionType type)
    {
        if (keyBindings.ContainsKey(key))
        {
            var actions = keyBindings[key];
            var wrapper = actions.FirstOrDefault(x => x.Type == type);
            if (wrapper != null)
            {
                wrapper += action;
            }
            else
            {
                actions.Add(new ActionWrapper(action, type));
            }
        }
        else
        {
            keyBindings.Add(key, new List<ActionWrapper>() { new(action, type) });
        }
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
                keyBindings[key].ForEach(x => x.Invoke(ActionWrapper.ActionType.Pressed));
                _pressedKey = key;
            }
            else if (Input.GetKeyUp(key))
            {
                keyBindings[key].ForEach(x => x.Invoke(ActionWrapper.ActionType.Released));
                _pressedKey = KeyCode.None;
            }
            else if (Input.GetKey(key))
            {
                keyBindings[key].ForEach(x => x.Invoke(ActionWrapper.ActionType.Held));
                _pressedKey = key;
            }
        }
    }
}
