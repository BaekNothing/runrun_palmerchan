using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UICoreSystem : MonoBehaviour
{
    GamePlayManager _gamePlayManager;
    GamePlayManager GamePlayManager
    {
        get
        {
            if (_gamePlayManager == null)
            {
                _gamePlayManager = this.GetComponent<GameCoreSystem>().GamePlayManager;
            }
            return _gamePlayManager;
        }
    }

    public void Pause()
    {
        GamePlayManager.GamePaused();
    }
}
