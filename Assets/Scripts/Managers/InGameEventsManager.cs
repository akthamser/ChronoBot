using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class InGameEventsManager : MonoBehaviour
{
    public static InGameEventsManager Instance;
    public float GameTime;
    public float Timer;

    public InGameEvent[] InGameEvents;

    private int NextEvent;

    [System.Serializable]
    public struct InGameEvent
    {
        public float StartTime;
        public UnityEvent StartEvent;
    }


    public UnityEvent EndGameEvent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NextEvent = 0;
    }

    public void Update()
    {
        Timer += Time.deltaTime;

        if (NextEvent < InGameEvents.Length && Timer > InGameEvents[NextEvent].StartTime)
        {
            InGameEvents[NextEvent].StartEvent.Invoke();
            NextEvent++;
        }


        if(Timer> GameTime)
            EndGameEvent.Invoke();
    }
}
