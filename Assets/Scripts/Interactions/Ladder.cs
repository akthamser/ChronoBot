using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public LadderPoint Point1;
    public LadderPoint Point2;
    public Transform startpoint,endpoint,startoffsetpoint,endoffsetpoint;
    public float UseTime = 2;
    public float UseTimer;
    private bool Interpolating;
    private float Delay;
    public AnimationCurve MoveYCurve;
    public float jumptoledderspeed;
    public float climbingspeed;

    public void UseLadder(LadderPoint point)
    {
        if(UseTimer < 0)
        {
            if (Point1 == point&&!Interpolating)
            {
                //MovmentController.Instance.GetComponent<Rigidbody>().position = Point2.TeleportPoint.position;
                StartCoroutine(IClimbeLedder(true,MoveYCurve,1));
                UseTimer = UseTime;
                return;
            }

            if (Point2 == point&&!Interpolating)
            {
                //MovmentController.Instance.GetComponent<Rigidbody>().position = Point1.TeleportPoint.position;
                StartCoroutine(IClimbeLedder(false, MoveYCurve, 1));
                UseTimer = UseTime;
            }

        }

    }
    IEnumerator IClimbeLedder(bool GoUp,AnimationCurve Ycurve, float Speed)
    {
        MovmentController.Instance.removecontroll();

        Interpolating = true;
        if (GoUp)
        {
            Vector3 _StartPosition = MovmentController.Instance.transform.position;
            Quaternion _StartRotation = MovmentController.Instance.transform.rotation;
            float _InterpolationValue = 0;
            yield return new WaitForSeconds(Delay);
            while (_InterpolationValue < 1)
            {
                _InterpolationValue += Time.deltaTime * jumptoledderspeed;
                MovmentController.Instance.transform.position = Vector3.Lerp(_StartPosition, startpoint.position, _InterpolationValue) + Vector3.up * Ycurve.Evaluate(_InterpolationValue);
                MovmentController.Instance.transform.rotation = Quaternion.Lerp(_StartRotation, startpoint.rotation, _InterpolationValue);
                yield return null;
            }
            _InterpolationValue = 0;
            _StartPosition = MovmentController.Instance.transform.position;
            while (_InterpolationValue < 1)
            {
                _InterpolationValue += Time.deltaTime * climbingspeed;
                MovmentController.Instance.transform.position = Vector3.Lerp(_StartPosition, endpoint.position, _InterpolationValue);
                yield return null;
            }

            _InterpolationValue = 0;
            _StartPosition = MovmentController.Instance.transform.position;
            _StartRotation = MovmentController.Instance.transform.rotation;
            while (_InterpolationValue < 1)
            {
                _InterpolationValue += Time.deltaTime * jumptoledderspeed;
                MovmentController.Instance.transform.position = Vector3.Lerp(_StartPosition, endoffsetpoint.position, _InterpolationValue) + Vector3.up * Ycurve.Evaluate(_InterpolationValue);
                MovmentController.Instance.transform.rotation = Quaternion.Lerp(_StartRotation, Quaternion.identity, _InterpolationValue);
                yield return null;
            }
        }
        else
        {
            Vector3 _StartPosition = MovmentController.Instance.transform.position;
            Quaternion _StartRotation = MovmentController.Instance.transform.rotation;
            float _InterpolationValue = 0;
            yield return new WaitForSeconds(Delay);
            while (_InterpolationValue < 1)
            {
                _InterpolationValue += Time.deltaTime * jumptoledderspeed;
                MovmentController.Instance.transform.position = Vector3.Lerp(_StartPosition, endpoint.position, _InterpolationValue) + Vector3.up * Ycurve.Evaluate(_InterpolationValue);
                MovmentController.Instance.transform.rotation = Quaternion.Lerp(_StartRotation, endpoint.rotation, _InterpolationValue);
                yield return null;
            }
            _InterpolationValue = 0;
            _StartPosition = MovmentController.Instance.transform.position;
            while (_InterpolationValue < 1)
            {
                _InterpolationValue += Time.deltaTime * climbingspeed;
                MovmentController.Instance.transform.position = Vector3.Lerp(_StartPosition, startpoint.position, _InterpolationValue);
                yield return null;
            }

            _InterpolationValue = 0;
            _StartPosition = MovmentController.Instance.transform.position;
            _StartRotation = MovmentController.Instance.transform.rotation;
            while (_InterpolationValue < 1)
            {
                _InterpolationValue += Time.deltaTime * jumptoledderspeed;
                MovmentController.Instance.transform.position = Vector3.Lerp(_StartPosition, startoffsetpoint.position, _InterpolationValue) + Vector3.up * Ycurve.Evaluate(_InterpolationValue);
                MovmentController.Instance.transform.rotation = Quaternion.Lerp(_StartRotation, Quaternion.identity, _InterpolationValue);
                yield return null;
            }
        }
      

        Interpolating = false;
        MovmentController.Instance.backcontroll();


    }

    private void Update()
    {
        UseTimer -= Time.deltaTime;
    }
}
