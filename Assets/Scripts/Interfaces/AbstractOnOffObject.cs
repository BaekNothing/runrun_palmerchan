using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractOnOffObject : MonoBehaviour
{
    void Awake()
    {
        Init();
    }

    protected abstract void Init();
}
