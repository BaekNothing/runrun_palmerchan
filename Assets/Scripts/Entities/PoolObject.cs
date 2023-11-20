using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolObject : MonoBehaviour
{
    [SerializeField] ObjectPoolSystem.PoolType _poolType;
    public ObjectPoolSystem.PoolType PoolType => _poolType;

    public void Init(ObjectPoolSystem.PoolType poolType)
    {
        _poolType = poolType;
        SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void OnParticleSystemStopped()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        ObjectPoolSystem.ReturnObject(_poolType, this);
    }
}
