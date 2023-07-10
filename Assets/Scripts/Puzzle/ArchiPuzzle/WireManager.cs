using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class ArchiPuzzleManager : Puzzle
{
    
    public static ArchiPuzzleManager instance;
    public WirePoint SelectedWire;
    public float DistenceToAutoWire;
    public List<Connectpoint> connectpoints = new List<Connectpoint>();
    public List<Gate> Gates = new List<Gate>();
    public bool Correct;
    [Header("Boundries")]
    public Transform Left,Right,Up,Down;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Correct = true;
        SelectedWire=null;
        Controller.Player.LockPlayerMovment();
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Resetwires();
            gameObject.SetActive(false);
        }


        if (Input.GetMouseButtonDown(1)&&SelectedWire!=null)
        {
            DiselectCurrentWire();
        }
        if (Input.GetMouseButtonDown(0) && SelectedWire != null)
        {

            Connectpoint nearstconnectpoint = TheNearestConnectPoint();

            if (Vector2.Distance(SelectedWire.transform.InverseTransformPoint(nearstconnectpoint.linkpoint.position), SelectedWire.mousepos) < DistenceToAutoWire)
            {
                SelectedWire.wirepointPositions[SelectedWire.wirepointPositions.Count - 1] = SelectedWire.transform.InverseTransformPoint(nearstconnectpoint.linkpoint.position);
                nearstconnectpoint.LinkedWire = SelectedWire;
                SelectedWire.Connectedpoint = nearstconnectpoint;
                SelectedWire.Selected = false;
                SelectedWire = null;

            }
            else
            {
                SelectedWire.wirepointPositions[SelectedWire.wirepointPositions.Count - 1] = SelectedWire.mousepos;
                SelectedWire.wirepointPositions.Add(SelectedWire.mousepos);

            }

        }
    }
    public void DiselectCurrentWire()
    {
        if (SelectedWire != null)
        {
        SelectedWire.WireRenderer.Points.RemoveRange(1, SelectedWire.WireRenderer.Points.Count - 1);
        SelectedWire.Selected = false;
        SelectedWire = null;

        }
    }

    private Connectpoint TheNearestConnectPoint()
    {
        float mindistence = 10000000000;
        Connectpoint nearestconnectpoint = null;
            foreach (Connectpoint connectpoint in connectpoints)
            {
            if (connectpoint.LinkedWire==null &&  Vector2.Distance(SelectedWire.transform.InverseTransformPoint(connectpoint.linkpoint.position), SelectedWire.mousepos) < mindistence)
            { 
                mindistence = Vector2.Distance(SelectedWire.transform.InverseTransformPoint(connectpoint.linkpoint.position), SelectedWire.mousepos);
                nearestconnectpoint = connectpoint;
            }

            }
            return nearestconnectpoint;
    }
    public void quit()
    {

        ArchiPuzzleManager.instance.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Controller.Player.UnLockPlayerMovment();
    }
    public void Resetwires()
    {
        foreach (Connectpoint connectpoint in connectpoints)
            connectpoint.DetatchWire();
    }
}
