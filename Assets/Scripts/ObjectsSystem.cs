using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsSystem : MonoBehaviour
{
    public static List<IBgObject> bgObjects = new();
    public GameObject _prefab;
    public Transform _ResetPosition;

    void Awake()
    {
        InputSystem.BindKey(KeyCode.LeftArrow, MoveLeft, InputSystem.ActionType.Held);
        InputSystem.BindKey(KeyCode.RightArrow, MoveRight, InputSystem.ActionType.Held);
        InputSystem.BindKey(KeyCode.Q, CreateObjectByPrefab, InputSystem.ActionType.Pressed);
    }

    void MoveLeft()
    {
        bgObjects.ForEach(x => x.Move(new Vector2(-1, 0)));
    }

    void MoveRight()
    {
        bgObjects.ForEach(x => x.Move(new Vector2(1, 0)));
    }

    void CreateObjectByPrefab()
    {
        var RandomDepth = UnityEngine.Random.Range(0.0f, 1.0f);
        var RandomPosition = UnityEngine.Random.Range(-1.0f, 1.0f);

        CreateObjectByPrefab(_prefab, new Vector2(RandomPosition, RandomPosition), RandomDepth);
    }

    public void CreateObjectByPrefab(GameObject prefab, Vector2 position, float depth)
    {
        GameObject gameObject = Instantiate(prefab, new Vector3(position.x, position.y, depth), Quaternion.identity);
        AddBgObject(gameObject, depth);
    }

    void AddBgObject(GameObject gameObject, float depth)
    {
        bgObjects.Add(new BgObject(gameObject, depth));
    }

    void DistoryObject(GameObject gameObject)
    {
        bgObjects.Remove(bgObjects.Find(x => x.GameObject == gameObject));
        Destroy(gameObject);
    }
}
