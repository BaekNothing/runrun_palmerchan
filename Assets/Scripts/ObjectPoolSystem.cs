using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(GameCoreSystem))]
public class ObjectPoolSystem : MonoBehaviour
{
    public enum PoolType
    {
        TouchEffect,
        TouchTail,
    }

    static readonly Dictionary<PoolType, Queue<PoolObject>> _pools = new();

    void Awake()
    {
        InitPool();
    }

    void InitPool()
    {
        InitTouchEffect();
    }

    void InitTouchEffect()
    {
        _pools.Add(PoolType.TouchEffect, new Queue<PoolObject>());
        for (int i = 0; i < ObjectData.Instance.TouchEffectPoolSize; i++)
        {
            var obj = Instantiate(ObjectData.Instance.TouchEffectPrefab);
            obj.Init(PoolType.TouchEffect);
            _pools[PoolType.TouchEffect].Enqueue(obj);
        }

        _pools.Add(PoolType.TouchTail, new Queue<PoolObject>());
        for (int i = 0; i < ObjectData.Instance.TouchTailPoolSize; i++)
        {
            var obj = Instantiate(ObjectData.Instance.TouchTailPrefab);
            obj.Init(PoolType.TouchTail);
            _pools[PoolType.TouchTail].Enqueue(obj);
        }
    }

#nullable enable
    public static PoolObject? GetObject(PoolType type)
    {
        if (_pools[type].Count == 0)
        {
            return null;
        }
        else
        {
            var obj = _pools[type].Dequeue();
            obj.SetActive(true);
            return obj;
        }
    }
#nullable disable

    public static void ReturnObject(PoolType type, PoolObject obj)
    {
        obj.SetActive(false);
        _pools[type].Enqueue(obj);
    }
}
