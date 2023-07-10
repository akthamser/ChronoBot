using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerUI : MonoBehaviour
{
    public int TimeWithSeconds;
    public TextMeshProUGUI TimeText;
    private float remainingTime;
    private float min, sec;
    public UnityEvent OnTimeEnd;
    private bool timedone;
    public bool  StopTime;
    void Start()
    {
        timedone = false;
        remainingTime = TimeWithSeconds;
        min = remainingTime / 60;
        sec = remainingTime % 60;
        StopTime = false;
    }


    void Update()
    {

        if (StopTime)
            return;
        if (remainingTime < 0&&!timedone)
        {
            timedone = true;
            OnTimeEnd.Invoke();
        }
        else
        {
            remainingTime -= Time.deltaTime;
            min = Mathf.Clamp(remainingTime / 60,0,float.PositiveInfinity) ;
            sec = Mathf.Clamp(remainingTime % 60, 0, float.PositiveInfinity);

            TimeText.text = Mathf.Floor(min).ToString() + " : " + sec.ToString("00");
        }


    }


}
