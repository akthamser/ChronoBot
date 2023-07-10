using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FinalTaskButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool Clicked;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        Clicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Clicked = false;
    }
}
