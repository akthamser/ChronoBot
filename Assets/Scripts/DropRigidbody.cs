using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DropRigidbody : MonoBehaviour
{
    private Rigidbody Body;
    public UnityEvent DropEvent;

    void Start()
    {
        Body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > 1.5f)
        {
            DropEvent.Invoke();
        }
    }
}
