using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectsManager : AbstractManager
{
    public List<AbstractObject> _objects = new();
    [SerializeField] Transform _resetPosition;
    public Transform ResetPosition
    {
        get
        {
            if (_resetPosition == null)
            {
                _resetPosition = GameObject.FindGameObjectWithTag(Utility.TAG_RESPAWN)?.transform;
            }
            return _resetPosition;
        }
    }


    public override void Init()
    {
        SetObject();
        BindPeriodicAction();
        IsReady = true;
    }

    void SetObject()
    {
        for (int i = 0; i < 10; i++)
        {
            var RandomDepth = UnityEngine.Random.Range(0.2f, 1.0f);
            CreateObjectByPrefab<BgObject>(
                GameData.Instance.BgObjectsPrefab, RandomDepth);
        }

        for (int i = 0; i < 3; i++)
        {
            CreateObjectByPrefab<ObstacleObject>(
                GameData.Instance.ObstaclePrefab, 1.0f);
        }

        for (int i = 0; i < 3; i++)
        {
            CreateObjectByPrefab<SupportObject>(
                GameData.Instance.SupportObjectPrefab, 1.0f);
        }
    }

    void BindPeriodicAction()
    {
        PeriodicActionManager.BindPeriodicAction(3.0, ActiveSupportObject);
        PeriodicActionManager.BindPeriodicAction(PeriodicActionManager.EVERY_FRAME, MoveLeft);
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
        var target = UnityEngine.Object.Instantiate(prefab.gameObject).GetComponent<T>();
        target.Init(depth, ResetPosition);
        _objects.Add(target);
    }

    public void ActiveSupportObject()
    {
        var target = _objects.Where(x => x.gameObject.activeSelf == false).FirstOrDefault();
        target?.gameObject.SetActive(true);
    }
}
