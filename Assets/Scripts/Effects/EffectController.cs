using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] List<GameObject> _effects;
    [SerializeField] Image_CutIn _cutIn;
    Func<int, bool> _cutInIsOn = (x) => false;

    public void PlayEffect(int index)
    {
        _effects.ForEach(x => x.SetActive(false));
        _effects[index].SetActive(true);

        if (_cutIn && (_cutInIsOn?.Invoke(index) ?? false))
        {
            _cutIn.Index = index;
            _cutIn.gameObject.SetActive(true);
        }
    }

    public void BindCutInIsOn(Func<int, bool> cutInIsOn)
    {
        _cutInIsOn = cutInIsOn;
    }

    public void SetCutInEndCallback(System.Action callback)
    {
        _cutIn.SetCutinEndCallback(callback);
    }
}
