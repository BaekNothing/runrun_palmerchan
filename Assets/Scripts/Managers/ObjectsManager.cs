using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectsManager : AbstractManager
{
    public List<AbstractObject> _objects = new();

    public override void Init()
    {
        SetObject();
        BindPeriodicAction();
        IsReady = true;
    }

    void SetObject()
    {
        for (int i = 0; i < ObjectData.Instance.BgObjectMaxCount; i++)
        {
            var RandomDepth = UnityEngine.Random.Range(
                ObjectData.Instance.BgObjectDepthRange.x,
                ObjectData.Instance.BgObjectDepthRange.y
            );
            CreateObjectByPrefab<BgObject>(
                ObjectData.Instance.BgObjectsPrefab, RandomDepth);
        }

        for (int i = 0; i < ObjectData.Instance.SupportObjectMaxCount; i++)
        {
            CreateObjectByPrefab<SupportObject>(
                ObjectData.Instance.SupportObjectPrefab,
                ObjectData.Instance.SupportObjectDepth
            );
        }
    }

    void BindPeriodicAction()
    {
        PeriodicActionManager.BindPeriodicAction(ObjectData.Instance.SupportObjectCreateDelay, ActiveSupportObject);
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
        target.Init(depth);
        _objects.Add(target);
    }

    public void ActiveSupportObject()
    {
        if (GameData.Instance.DashTime > 0) return;

        if (ObjectData.Instance.CheckSupportObject())
        {
            var target = _objects.Where(x => !x?.gameObject?.activeSelf ?? false).FirstOrDefault();
            target?.Reset();
            target?.gameObject.SetActive(true);
        }
    }
}
