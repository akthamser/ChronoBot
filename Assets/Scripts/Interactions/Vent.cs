using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : InteractionsController
{
    public AnimationCurve MoveYCurve;
    public Transform ExitPoint;
    public Vector3 PositionOffset;
    public float Speed;
    public AnimationCurve SpeedCurve;
    public float Delay = 1;
    public float UseCoolDown = 0.5f;
    public MazeManager LinkedMaze;
    public Room Room;
    [HideInInspector]
    public float CoolDownCountDown;
    private Animator VentAnimator;

    private void Start()
    {
        VentAnimator = GetComponent<Animator>();
    }
    public void playventsound()
    {
        AudioManager.Instance.PlaySound(gameObject.name);
    }


    public void EnterVent()
    {
        if (!Interacting && !Interpolating && CoolDownCountDown < 0 )
        {
            MoveToPositon(transform.position + PositionOffset, MoveYCurve, Speed,false,false);
            MovmentController.Instance.LockPlayerMovment();
            VentAnimator.SetTrigger("Open");

            CoolDownCountDown = UseCoolDown;
        }
    }
    
    public void EnterVentDontCheckInteracting()
    {
        if ( !Interpolating && CoolDownCountDown < 0)
        {
            MoveToPositon(transform.position + PositionOffset, MoveYCurve, Speed, false, false);
            MovmentController.Instance.LockPlayerMovment();
            VentAnimator.SetTrigger("Open");

            CoolDownCountDown = UseCoolDown;
        }
    }


    public void EnterMazeVent(bool fromstart)
    {
        if ( !Interpolating && CoolDownCountDown < 0)
        {
            MoveToPositon(transform.position + PositionOffset, MoveYCurve, Speed,true,fromstart);
            MovmentController.Instance.LockPlayerMovment();
            VentAnimator.SetTrigger("Open");

            CoolDownCountDown = UseCoolDown;
        }
    }


    public void ExitVent()
    {
        if (Interacting && !Interpolating && CoolDownCountDown < 0)
        {
            MoveToPositon(ExitPoint.position, MoveYCurve, Speed,false,false);
           
            VentAnimator.SetTrigger("Open");

            CoolDownCountDown = UseCoolDown;
        }
    }
    public void ExitMazeVent()
    {
        MoveToPositon(ExitPoint.position, MoveYCurve, Speed,false,false);

        VentAnimator.SetTrigger("Open");

        CoolDownCountDown = UseCoolDown;

        
    }
   


    private void Update()
    {
        base.BaseUpdate();

        CoolDownCountDown -= Time.deltaTime;
    }

  public bool Interpolating;
    public void MoveToPositon(Vector3 Position, AnimationCurve Ycurve, float Speed,bool EnterMaze,bool fromstart)
    {
        StartCoroutine(IMoveToPosition(Position, Ycurve, Speed,EnterMaze, fromstart));

    }

    IEnumerator IMoveToPosition(Vector3 Position, AnimationCurve Ycurve, float Speed,bool EnterMaze,bool fromstart)
    {
        Interpolating = true;
        Vector3 _StartPosition = MovmentController.Instance.transform.position;
        float _InterpolationValue = 0;
        yield return new WaitForSeconds(Delay);

        bool transition=false;
        while (_InterpolationValue < 1)
        {
            _InterpolationValue += Time.deltaTime * Speed;
            MovmentController.Instance._rigidbody.position = Vector3.Lerp(_StartPosition, Position, _InterpolationValue) + Vector3.up * Ycurve.Evaluate(_InterpolationValue);
            if (EnterMaze&&_InterpolationValue>0.5f&&!transition)
            {
                transition = true;
                RoomsController.Instance.ActivateRoom(LinkedMaze.GetComponent<Room>());
            }
            yield return null;
        }
        
        Interpolating = false;

        Interacting = !Interacting;


        if (!Interacting)
        {
            MovmentController.Instance.UnLockPlayerMovment();
        }
        if (EnterMaze)
        {

            LinkedMaze.StartMaze(fromstart);
            Interacting = false;
        }
    }

}
