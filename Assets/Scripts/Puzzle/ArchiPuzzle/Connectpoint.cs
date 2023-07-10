using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Connectpoint : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public WirePoint LinkedWire;
    private bool Hovered;
    public Transform linkpoint;
    public GameObject WireEnd;
    private void OnEnable()
    {
    }
    private void Start()
    {
        ArchiPuzzleManager.instance.connectpoints.Add(this);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && LinkedWire != null&&Hovered)
            DetatchWire();

        if (LinkedWire != null)
            WireEnd.SetActive(false);
        else
            WireEnd.SetActive(true);
    }

    public void DetatchWire()
    {
        if (LinkedWire != null)
        {

        LinkedWire.WireRenderer.Points.RemoveRange(1, LinkedWire.WireRenderer.Points.Count - 1);
        LinkedWire.Connectedpoint = null;
        LinkedWire =null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hovered = false;
    }
    private void OnDisable()
    {
        DetatchWire();
    }
  
}
