using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public abstract class AbstractText : MonoBehaviour
{
    void Awake()
    {
        Init();
    }

    protected abstract void Init();
}
