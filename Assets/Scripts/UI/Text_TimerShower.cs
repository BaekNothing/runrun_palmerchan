using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_TimerShower : AbstractText
{
    [SerializeField] Text _text = null;
    Text Text
    {
        get
        {
            if (_text == null)
                _text = GetComponent<Text>();
            return _text;
        }
    }

    protected override void Init()
    {
        PeriodicActionManager.BindPeriodicAction(0.1, UpdateScore);
    }

    void UpdateScore()
    {
        var time = GameData.Instance.TimeLimit - GameData.Instance.Timer;
        int minute = (int)time / 60;
        int second = (int)time % 60;

        Text.text = $"{minute:D2}:{second:D2}";
    }
}
