using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InteractionsController : MonoBehaviour
{
    [Header("InteractionsConrtoller")]
    public KeyCode StartInteractKey = KeyCode.E;
    public KeyCode EndInteractKey = KeyCode.Escape;

    public UnityEvent OnStartInteracting;
    public UnityEvent OnEndInteracting;

    public string InteractingText = "to enter the vent";
    private bool Inrange;
    public bool Interacting;




    private void Awake()
    {
        EndInteractKey = KeyCode.E;
    }
    public void BaseUpdate()
    {
        if (Input.GetKeyDown(StartInteractKey) && Inrange)
        {
            OnStartInteracting.Invoke();
        }

        if (Input.GetKeyDown(EndInteractKey))
        {
            OnEndInteracting.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Inrange = true;
            UserFeedBack.Instance.SetText("Press E to " + InteractingText);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Inrange = false;
            UserFeedBack.Instance.DisableText();
        }

    }
}
