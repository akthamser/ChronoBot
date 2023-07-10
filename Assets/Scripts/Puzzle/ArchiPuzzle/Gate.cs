using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Connectpoint[] Gateinputs;
    public WirePoint Gateoutput;
    public WirePoint[] CorrectInputs;
    public bool CorrectGate;

    private void Start()
    {
        ArchiPuzzleManager.instance.Gates.Add(this);
    }


    public void Update()
    {
        bool correct = true;
        foreach (Connectpoint input in Gateinputs)
        {
            if (!CorrectInputs.Contains<WirePoint>(input.LinkedWire))
                correct = false;

        }
        CorrectGate=correct;
    }
}
