using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class WirePoint : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UILineRenderer WireRenderer;
    public Color WireColor;
    public bool Selected ;
    public bool value;
    public LayerMask wirepoint;
    public List<Vector2> wirepointPositions = new List<Vector2>();
    public Vector2 mousepos;
    private bool Hovered;
    public Connectpoint Connectedpoint;
    public Transform linkpoint;
    public GameObject WireEnd;


    private void OnEnable()
    {
        wirepointPositions.Clear();
        WireRenderer = GetComponent<UILineRenderer>();
        WireRenderer.color= WireColor;
        wirepointPositions.Add(transform.InverseTransformPoint(linkpoint.position));
      
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    private void Update()
    {
        if (Selected)
            wirepointPositions[wirepointPositions.Count-1] = mousepos;

        WireRenderer.SetPoints(wirepointPositions);
        mousepos = new Vector3(Mathf.Clamp(transform.InverseTransformPoint(Input.mousePosition).x, transform.InverseTransformPoint(ArchiPuzzleManager.instance.Left.transform.position).x, transform.InverseTransformPoint(ArchiPuzzleManager.instance.Right.transform.position).x), Mathf.Clamp(transform.InverseTransformPoint(Input.mousePosition).y, transform.InverseTransformPoint(ArchiPuzzleManager.instance.Down.transform.position).y, transform.InverseTransformPoint(ArchiPuzzleManager.instance.Up.transform.position).y));

        if (Input.GetMouseButtonDown(1) && Connectedpoint!=null && Hovered)
            Connectedpoint.DetatchWire();

        if (!Selected && Connectedpoint == null && Input.GetMouseButtonDown(0)&&(Hovered))
        {
            ArchiPuzzleManager.instance.DiselectCurrentWire();
            Selected = true;
            wirepointPositions.Add((Vector2)Input.mousePosition - (Vector2)transform.position);
            ArchiPuzzleManager.instance.SelectedWire = this;

        }
        if (Connectedpoint != null || Selected)
            WireEnd.SetActive(false);
        else
            WireEnd.SetActive(true);


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
        Selected = false;
        WireEnd.SetActive(true);
    }
}
