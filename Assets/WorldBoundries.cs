using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundries : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovmentController.Instance.Restart();
        }
    }
}
