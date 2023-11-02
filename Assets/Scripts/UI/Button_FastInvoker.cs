using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_FastInvoker : Button, IPointerDownHandler
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        onClick.Invoke();
    }
}
