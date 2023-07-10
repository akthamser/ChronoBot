using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterKillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MovmentController.Instance.Die();
        }
    }
}
