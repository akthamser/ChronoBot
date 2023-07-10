using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTask : Puzzle
{
    public FinalTaskButton button;
    public Transform PressureArrow;
    public Transform CorrectAngleTransform;
    public float RotationSpeed = 3;
    public float TargetCorrectAngle;
    public float CorrectAngleRotationSpeed = 5;
    public float CorrectAngle = 30;



    public Slider[] PressureSliders;
    public int ChangedTimes = 3;
    public float TimeToChange = 4;
    public float TimerToChange;
    public void OnEnable()
    {
        ChangedTimes = PressureSliders.Length -1;
        ChooseRndCorrectAngle();

        for(int i = 0; i < PressureSliders.Length; i++)
        {
            PressureSliders[i].value = 1;
        }
    }


    public void ChooseRndCorrectAngle()
    {
        TimerToChange = TimeToChange;
        TargetCorrectAngle = Random.Range(0, 360.0f);

    }

    public void Update()
    {

        if (button.Clicked)
        {
            PressureArrow.transform.eulerAngles += Vector3.forward * RotationSpeed * Time.deltaTime;
        }
        else
        {
            PressureArrow.transform.eulerAngles -= Vector3.forward * RotationSpeed * Time.deltaTime;
        }

        if (Vector3.Angle(PressureArrow.up, CorrectAngleTransform.up) < CorrectAngle)
        {
            TimerToChange -= Time.deltaTime;
            if(TimerToChange < 0)
            {
                if(ChangedTimes > 0)
                {
                    ChooseRndCorrectAngle();
                    ChangedTimes--;
                }
                else
                {
                    Controller.OnPuzzleDone.Invoke();
                    gameObject.SetActive(false);
                }
            }
        }
        else
            TimerToChange = TimeToChange;

        CorrectAngleTransform.transform.eulerAngles = Vector3.Lerp(CorrectAngleTransform.transform.eulerAngles, Vector3.forward *TargetCorrectAngle,CorrectAngleRotationSpeed*Time.deltaTime) ;
        PressureSliders[ChangedTimes].value = TimerToChange /TimeToChange;

        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
        }

    }

    private void OnDisable()
    {
        Controller.Player.UnLockPlayerMovment();

    }
}
