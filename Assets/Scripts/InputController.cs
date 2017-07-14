using System;
using System.Collections.Generic;
using CnControls;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool AcceptInput = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        AcceptInput = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AcceptInput = false;
    }
}