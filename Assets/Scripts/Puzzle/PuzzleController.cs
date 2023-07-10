using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleController : MonoBehaviour
{

    public Puzzle ThePuzzle;
    [HideInInspector]
    public MovmentController Player;
    public KeyCode InteracteKey ;
    public UnityEvent OnPuzzleDone;

    private bool Inrange;

    void Start()
    {
        Inrange = false;
        ThePuzzle.Controller = this;
        Player = MovmentController.Instance;
    }

    void Update()
    {
        if(Input.GetKeyDown(InteracteKey)&&Inrange)
        {

            Player.LockPlayerMovment();
            ThePuzzle.gameObject.SetActive(true);

            UserFeedBack.Instance.DisableText();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             Inrange = true;
            UserFeedBack.Instance.SetText("Press E to Use the machine");
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
