using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsSystem : MonoBehaviour
{
    public static List<IBgObject> _bgObjects = new();
    public BgObject _prefab;
    public Transform _ResetPosition;

    void Awake()
    {
        InputSystem.BindKey(KeyCode.LeftArrow, MoveLeft, InputSystem.ActionType.Held);
        InputSystem.BindKey(KeyCode.RightArrow, MoveRight, InputSystem.ActionType.Held);
        InputSystem.BindKey(KeyCode.Q, CreateObjectByPrefab, InputSystem.ActionType.Pressed);
    }

    void MoveLeft()
    {
        Debug.Log($"{_bgObjects.Count} MoveLeft");
        _bgObjects.ForEach(x => x.Move(new Vector2(-1, 0)));
    }

    void MoveRight()
    {
        Debug.Log($"{_bgObjects.Count} MoveRight");
        _bgObjects.ForEach(x => x.Move(new Vector2(1, 0)));
    }

    void CreateObjectByPrefab()
    {
        var RandomDepth = UnityEngine.Random.Range(0.0f, 1.0f);
        var RandomPosition = UnityEngine.Random.Range(-1.0f, 1.0f);

        CreateObjectByPrefab(_prefab, new Vector2(RandomPosition, RandomPosition), RandomDepth);
    }

    public void CreateObjectByPrefab(BgObject prefab, Vector2 position, float depth)
    {
        var target = Instantiate(prefab, new Vector3(position.x, position.y, depth), Quaternion.identity);
        target.Init(depth, _ResetPosition);
        _bgObjects.Add(target);
    }
}
