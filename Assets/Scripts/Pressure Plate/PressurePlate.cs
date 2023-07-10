using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PressurePlate : MonoBehaviour
{


    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    private Animator m_Animator;
    private bool pressed = false;
    private List<GameObject> pressingobjects;
    public bool WaitTimeToActivate;
    public float WaitTime;
    public UnityEvent OnTimeElapsed;
    public bool TimeIndicator;
    public TimerIcon timerIcon;
    private float timer;



    private void Awake()
    {
        pressingobjects = new List<GameObject>();
        m_Animator = GetComponent<Animator>();
        timerIcon.gameObject.SetActive(false);

    }
    private void Update()
    {
       
        if (pressingobjects.Count == 0)
        {
            if (pressed)
            {
                OnRelease.Invoke();
                m_Animator.Play("Release");
                timer = 0;
                StopAllCoroutines();
                timerIcon.gameObject.SetActive(false);
                timerIcon.on = false;
                
            }

            pressed = false;
        }
        else
        {
            if (!pressed)
            {
                OnPress.Invoke();
                m_Animator.Play("Press");
                if(TimeIndicator)
                timerIcon.gameObject.SetActive(true);
                timerIcon.on = true;
                StartCoroutine(StartCountDown());
            }

            pressed = true;
        }
        if (pressed && WaitTimeToActivate)
        {
            timer += Time.deltaTime;
            timerIcon.value = timer/WaitTime;
        }
              
    }
    private void OnTriggerEnter(Collider other)
    {
        pressingobjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        pressingobjects.Remove(other.gameObject);

    }
    public IEnumerator  StartCountDown()
    { 
        yield return new WaitForSeconds(WaitTime);
        OnTimeElapsed.Invoke();
        yield return null;
        timerIcon.gameObject.SetActive(false);
    }
}
