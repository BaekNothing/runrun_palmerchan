using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class InputManager : AbstractManager
{
    [SerializeField] KeyCode _pressedKey = KeyCode.None;

    public override void Init()
    {
        IsReady = true;
    }

    static readonly Dictionary<KeyCode, List<ActionWrapper>> _keyBindings = new();
    static readonly Dictionary<MouseButton, List<ActionWrapper>> _mouseBindings = new();

    public static void BindKey(KeyCode key, Action action, ActionWrapper.ActionType type)
    {
        if (key == KeyCode.None && type == ActionWrapper.ActionType.Released)
            throw new Exception("Invalid Key Binding, None Key can't be released");
        else if (key.ToString().Contains("mouse"))
            throw new Exception("Invalid Key Binding, Mouse Key can't be binded with this method");

        if (_keyBindings.ContainsKey(key))
        {
            var actions = _keyBindings[key];
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
            _keyBindings.Add(key, new List<ActionWrapper>() { new(action, type) });
        }
    }

    public static void UnbindKey(KeyCode key, Action action)
    {
        if (_keyBindings.ContainsKey(key))
        {
            var actions = _keyBindings[key];
            var wrapper = actions.FirstOrDefault(x => x.Type == ActionWrapper.ActionType.Released);
            if (wrapper != null)
            {
                wrapper -= action;
            }
        }
    }

    public static void UnbindKey(KeyCode key)
    {
        if (_keyBindings.ContainsKey(key))
        {
            _keyBindings.Remove(key);
        }
    }

    public static void BindMouse(MouseButton button, Action action, ActionWrapper.ActionType type)
    {
        if (_mouseBindings.ContainsKey(button))
        {
            var actions = _mouseBindings[button];
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
            _mouseBindings.Add(button, new List<ActionWrapper>() { new(action, type) });
        }
    }

    public static void UnbindMouse(MouseButton button, Action action)
    {
        if (_mouseBindings.ContainsKey(button))
        {
            var actions = _mouseBindings[button];
            var wrapper = actions.FirstOrDefault(x => x.Type == ActionWrapper.ActionType.Released);
            if (wrapper != null)
            {
                wrapper -= action;
            }
        }
    }

    public static void UnbindMouse(MouseButton button)
    {
        if (_mouseBindings.ContainsKey(button))
        {
            _mouseBindings.Remove(button);
        }
    }

    public override void UpdateAction()
    {
        InputKey();
        InputAnyKey();
        InputMouse();
    }

    void InputKey()
    {
        foreach (var key in _keyBindings.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                _keyBindings[key].ForEach(x => x.Invoke(ActionWrapper.ActionType.Pressed));
                _pressedKey = key;
            }
            else if (Input.GetKeyUp(key))
            {
                _keyBindings[key].ForEach(x => x.Invoke(ActionWrapper.ActionType.Released));
                _pressedKey = KeyCode.None;
            }
            else if (Input.GetKey(key))
            {
                _keyBindings[key].ForEach(x => x.Invoke(ActionWrapper.ActionType.Held));
                _pressedKey = key;
            }
        }
    }

    void InputAnyKey()
    {
        if (Input.anyKeyDown)
        {
            var key = KeyCode.None;
            if (_keyBindings.ContainsKey(key))
            {
                _keyBindings[key].ForEach(x => x.Invoke(ActionWrapper.ActionType.Pressed));
                _pressedKey = key;
            }
        }
        else if (Input.anyKey)
        {
            var key = KeyCode.None;
            if (_keyBindings.ContainsKey(key))
            {
                _keyBindings[key].ForEach(x => x.Invoke(ActionWrapper.ActionType.Held));
                _pressedKey = key;
            }
        }
    }

    void InputMouse()
    {
        foreach (var button in _mouseBindings.Keys)
        {
            if (Input.GetMouseButtonDown((int)button))
            {
                _mouseBindings[button].ForEach(x => x.Invoke(ActionWrapper.ActionType.Pressed));
            }
            else if (Input.GetMouseButtonUp((int)button))
            {
                _mouseBindings[button].ForEach(x => x.Invoke(ActionWrapper.ActionType.Released));
            }
            else if (Input.GetMouseButton((int)button))
            {
                _mouseBindings[button].ForEach(x => x.Invoke(ActionWrapper.ActionType.Held));
            }
        }
    }
}
