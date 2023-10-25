using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum ActionType
    {
        None,
        Pressed,
        Released,
        Held
    }

    public interface IActionWrapper
    {
        public ActionType Type { get; }
        public void Invoke(ActionType type);
    }

    public class ActionWrapper : IActionWrapper
    {
        private readonly Action action;
        private readonly ActionType type;
        private bool invoked = false;

        public ActionType Type => type;

        public ActionWrapper(Action action, ActionType type)
        {
            this.action = action;
            this.type = type;
        }

        public void Invoke(ActionType type)
        {
            if (invoked || type == ActionType.None)
                return;

            if (type == this.type)
            {
                action.Invoke();
            }
        }
    }

    static readonly Dictionary<KeyCode, IActionWrapper> keyBindings = new();

    public static void BindKey(KeyCode key, Action action, ActionType type)
    {
        keyBindings.Add(key, new ActionWrapper(action, type));
    }

    public static void UnbindKey(KeyCode key)
    {
        keyBindings.Remove(key);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var key in keyBindings.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                keyBindings[key].Invoke(ActionType.Pressed);
            }
            else if (Input.GetKeyUp(key))
            {
                keyBindings[key].Invoke(ActionType.Released);
            }
            else if (Input.GetKey(key))
            {
                keyBindings[key].Invoke(ActionType.Held);
            }
            else
            {
                keyBindings[key].Invoke(ActionType.None);
            }
        }
    }
}
