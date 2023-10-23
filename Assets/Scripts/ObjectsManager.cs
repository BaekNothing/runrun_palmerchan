using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsManager : MonoBehaviour
{
    public static List<IBgObject> _bgObjects = new();
    public BgObject _prefab;
    public Transform _ResetPosition;

    void Awake()
    {
        // InputManager.BindKey(KeyCode.LeftArrow, MoveLeft, InputManager.ActionType.Held);
        // InputManager.BindKey(KeyCode.RightArrow, MoveRight, InputManager.ActionType.Held);
        // InputManager.BindKey(KeyCode.Q, CreateObjectByPrefab, InputManager.ActionType.Pressed);
        StartCoroutine(SetObject());
    }

    IEnumerator SetObject()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            CreateObjectByPrefab();
        }
    }

    void Update()
    {
        MoveLeft();
    }

    void MoveLeft()
    {
        Debug.Log($"{_bgObjects.Count} MoveLeft");
        _bgObjects.ForEach(x => x.Move(new Vector2(-PlayerData.Instance.Speed, 0)));
    }

    void MoveRight()
    {
        Debug.Log($"{_bgObjects.Count} MoveRight");
        _bgObjects.ForEach(x => x.Move(new Vector2(PlayerData.Instance.Speed, 0)));
    }

    void CreateObjectByPrefab()
    {
        var RandomDepth = UnityEngine.Random.Range(0.2f, 1.0f);
        CreateObjectByPrefab(_prefab, RandomDepth);
    }

    public void CreateObjectByPrefab(BgObject prefab, float depth)
    {
        var target = Instantiate(prefab);
        target.Init(depth, _ResetPosition);
        _bgObjects.Add(target);
    }
}
