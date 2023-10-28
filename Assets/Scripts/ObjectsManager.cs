using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsManager : MonoBehaviour
{
    public List<AbstractObject> _objects = new();
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
        _objects.ForEach(x => x.Move(new Vector2(-GameData.Instance.Speed, 0)));
    }

    void MoveRight()
    {
        _objects.ForEach(x => x.Move(new Vector2(GameData.Instance.Speed, 0)));
    }

    public void CreateObjectByPrefab<T>(AbstractObject prefab, float depth) where T : AbstractObject
    {
        var target = Instantiate(prefab.gameObject).GetComponent<T>();
        target.Init(depth, _ResetPosition);
        _objects.Add(target);
    }

    public void ActiveSupportObject()
    {
        var target = _objects.Where(x => x.gameObject.activeSelf == false).FirstOrDefault();
        target?.gameObject.SetActive(true);
    }
}
