using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] List<GameObject> _effects;

    public void PlayEffect(int index)
    {
        _effects.ForEach(x => x.SetActive(false));
        _effects[index].SetActive(true);
    }
}
