using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance;
    public TimerUI timer;
    private Animator EndGameAnimator;
 
    private void Start()
    {
        EndGameAnimator = GetComponent<Animator>();    
    }

    public void End()
    {
        MovmentController.Instance.LockPlayerMovment();
        EndGameAnimator.SetTrigger("EndGame");
        timer.StopTime = true;
        
    }
 

    public void restart()
    {
        
        MovmentController.Instance.Restart();
    }
}
