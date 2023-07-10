using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public Vent EntreeVent,StartVent;
    public Vent ExitVent,EndVent;
    public Transform StartPosition;
    public Transform EndPosition;
    public void StartMaze(bool fromthestart)
    {
        MovmentController.Instance.Canjump = false;
        if (fromthestart)
        {
             MovmentController.Instance.LockPlayerMovment();
            MovmentController.Instance.transform.position = StartPosition.position - StartPosition.transform.up * 0.5f;
             MovmentController.Instance.GetComponent<Rigidbody>().position = StartPosition.position - StartPosition.transform.up*0.5f;
             StartVent.ExitMazeVent();
             MovmentController.Instance.UnLockPlayerMovment();
        //Camera Prio
        }else
        {
            MovmentController.Instance.LockPlayerMovment();
            MovmentController.Instance.transform.position = EndPosition.position - EndPosition.transform.up * 0.5f;
            MovmentController.Instance.GetComponent<Rigidbody>().position = EndPosition.position - EndPosition.transform.up * 0.5f;
            EndVent.ExitMazeVent();
            MovmentController.Instance.UnLockPlayerMovment();

        }
        

    }
    public void ExitMaze(bool exitvent)
    {
        MovmentController.Instance.Canjump = true;
        StartCoroutine(IExitMaze(exitvent));
    }

    public IEnumerator IExitMaze(bool exitvent)
    {
        if (exitvent)
        {
            if (EndVent.CoolDownCountDown < 0)
            {

            MovmentController.Instance.LockPlayerMovment();
            EndVent.EnterVentDontCheckInteracting();
            yield return new WaitForSeconds(1);
            RoomsController.Instance.ActivateRoom(ExitVent.Room);
            MovmentController.Instance.transform.position = ExitVent.transform.position - ExitVent.transform.up * 0.5f;
            MovmentController.Instance.GetComponent<Rigidbody>().position = ExitVent.transform.position - ExitVent.transform.up * 0.5f;
            ExitVent.ExitMazeVent();
            MovmentController.Instance.UnLockPlayerMovment();
            ExitVent.Interacting = false;
            EndVent.Interacting = false;
            }

        }
        else
        {
            if (StartVent.CoolDownCountDown < 0)
            {


            MovmentController.Instance.LockPlayerMovment();
            StartVent.EnterVentDontCheckInteracting();
            yield return new WaitForSeconds(1);
            RoomsController.Instance.ActivateRoom(EntreeVent.Room);
            MovmentController.Instance.transform.position = EntreeVent.transform.position - EntreeVent.transform.up * 0.5f;
            MovmentController.Instance.GetComponent<Rigidbody>().position = EntreeVent.transform.position - EntreeVent.transform.up * 0.5f;
            EntreeVent.ExitMazeVent();
            MovmentController.Instance.UnLockPlayerMovment();
            StartVent.Interacting = false;
            EntreeVent.Interacting = false;
            }
        }
       

    }


}
