using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Text_ScoreShower : MonoBehaviour
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

    void Awake()
    {
        PeriodicActionManager.BindPeriodicAction(0.1, UpdateScore);
    }

    void UpdateScore()
    {
        Text.text = GameData.Instance.Score.ToString(Utility.SCORE_FORMAT);
    }
}
