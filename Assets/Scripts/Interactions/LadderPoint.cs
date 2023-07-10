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
            UserFeedBack.Instance.SetText("Press E To Use The Ledder");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanUse = false;
            UserFeedBack.Instance.DisableText();
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
