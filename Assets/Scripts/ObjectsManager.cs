using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsManager : MonoBehaviour
{
    public static List<IBgObject> _bgObjects = new();
    public BgObject _bgObjectPrefab;
    public ObstacleObject _obstacleObjectPrefab;
    public Transform _ResetPosition;

    void Awake()
    {
        StartCoroutine(SetObject());
    }

    IEnumerator SetObject()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var RandomDepth = UnityEngine.Random.Range(0.2f, 1.0f);
            CreateObjectByPrefab<BgObject>(_bgObjectPrefab, RandomDepth);
        }

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            CreateObjectByPrefab<ObstacleObject>(_obstacleObjectPrefab, 1.0f);
        }
    }

    void Update()
    {
        MoveLeft();
    }

    void MoveLeft()
    {
        _bgObjects.ForEach(x => x.Move(new Vector2(-PlayerData.Instance.Speed, 0)));
    }

    void MoveRight()
    {
        _bgObjects.ForEach(x => x.Move(new Vector2(PlayerData.Instance.Speed, 0)));
    }

    public void CreateObjectByPrefab<T>(IBgObject prefab, float depth) where T : IBgObject
    {
        var target = Instantiate(prefab.GameObject).GetComponent<T>();
        target.Init(depth, _ResetPosition);
        _bgObjects.Add(target);
    }
}
