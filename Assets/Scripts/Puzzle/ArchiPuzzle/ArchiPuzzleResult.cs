using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ArchiPuzzleResult : MonoBehaviour
{
    [Serializable]
    public struct Result
    {
        public Connectpoint resulteConnectPoint;
        public WirePoint CorrectResult;
    }
    public Result[] results;


    public void Submit()
    {
        bool correct = true;
        foreach (Result result in results)
        {
            if (result.CorrectResult != result.resulteConnectPoint.LinkedWire)
            { 
                correct = false;
            }
        }
        foreach (Gate gate in ArchiPuzzleManager.instance.Gates)
        {
            if (!gate.CorrectGate) correct = false;
        }
        if (correct)
        {
            ArchiPuzzleManager.instance.Controller.OnPuzzleDone.Invoke();
            ArchiPuzzleManager.instance.gameObject.SetActive(false);
            AudioManager.Instance.PlaySound("taskdone");

        }
        else
        { 
                foreach(Connectpoint point in ArchiPuzzleManager.instance.connectpoints)
                    point.DetatchWire();
            AudioManager.Instance.PlaySound("error");
            //wrong
        }

    }
}
