using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager could be a singleton or static class or something else. <br/>
/// but, I want to see it in the inspector. <br/>
/// so, I made it serializable. <br/>
/// </summary>
public abstract class AbstractManager
{
    public abstract void Init();
    [SerializeField] bool _isReady = false;
    public bool IsReady { get => _isReady; protected set => _isReady = value; }
    public abstract void UpdateAction();
}
