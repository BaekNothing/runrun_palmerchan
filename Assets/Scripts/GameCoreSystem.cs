using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// GameCoreSystem is just Start and Update.
/// It's a manager of managers.
/// If you want to add a new manager, you should add it to GameCoreSystemManager.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(EventSystem))]
[RequireComponent(typeof(ObjectPoolSystem))]
public class GameCoreSystem : MonoBehaviour
{
    [SerializeField] PeriodicActionManager _periodicActionManager = new();
    [SerializeField] ObjectsManager _objectsManager = new();
    [SerializeField] InputManager _inputManager = new();
    [SerializeField] GamePlayManager _gamePlayManager = new();


    void Awake()
    {
        Time.timeScale = 0f;
    }

    // For making sure that all init which are called in Awake() are done.
    void Start()
    {
        _objectsManager.Init();
        _gamePlayManager.Init();
        _inputManager.Init();
        _periodicActionManager.Init();
    }

    void Update()
    {
        if (!CheckAllManagerAreReady())
            return;
        if (Utility.CheckGameIsPaused())
            return;

        _periodicActionManager.UpdateAction();
    }

    bool CheckAllManagerAreReady()
    {
        return
            _periodicActionManager.IsReady &&
            _objectsManager.IsReady &&
            _inputManager.IsReady &&
            _gamePlayManager.IsReady;
    }

}
