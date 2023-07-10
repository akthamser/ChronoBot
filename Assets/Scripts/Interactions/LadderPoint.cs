using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPoint : MonoBehaviour
{
    public Ladder TargetLader;
    public Transform TeleportPoint;

    public bool CanUse;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanUse = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanUse = false;
        }
    }

    public void LateUpdate()
    {
        if (CanUse && Input.GetKeyDown(KeyCode.E))
        {
            TargetLader.UseLadder(this);
        }
    }
}
