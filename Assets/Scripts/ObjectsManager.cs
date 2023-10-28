using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsManager : MonoBehaviour
{
    public static List<IBgObject> _bgObjects = new();
    public Transform _ResetPosition;

    void Awake()
    {
        StartCoroutine(SetObject());
        GamePlayManager.BindPeriodicAction(3.0, ActiveSupportObject);
    }

    IEnumerator SetObject()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var RandomDepth = UnityEngine.Random.Range(0.2f, 1.0f);
            CreateObjectByPrefab<BgObject>(
                GameData.Instance.BgObjectsPrefab, RandomDepth);
        }

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            CreateObjectByPrefab<ObstacleObject>(
                GameData.Instance.ObstaclePrefab, 1.0f);
        }

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            CreateObjectByPrefab<SupportObject>(
                GameData.Instance.SupportObjectPrefab, 1.0f);
        }
    }

    void Update()
    {
        MoveLeft();
    }

    void MoveLeft()
    {
        _bgObjects.ForEach(x => x.Move(new Vector2(-GameData.Instance.Speed, 0)));
    }

    void MoveRight()
    {
        _bgObjects.ForEach(x => x.Move(new Vector2(GameData.Instance.Speed, 0)));
    }

    public void CreateObjectByPrefab<T>(IBgObject prefab, float depth) where T : IBgObject
    {
        var target = Instantiate(prefab.GameObject).GetComponent<T>();
        target.Init(depth, _ResetPosition);
        _bgObjects.Add(target);
    }

    public void ActiveSupportObject()
    {
        var target = _bgObjects.Where(x => x.GameObject.activeSelf == false).FirstOrDefault();
        target?.GameObject.SetActive(true);
    }
}
