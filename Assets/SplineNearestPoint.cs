using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;
public class SplineNearestPoint : MonoBehaviour
{
    public SplineContainer spline;
    public Transform Target;

    public float3 NearestPoint;
    public float t;
    [Header("Movement")]
    public float FollowSpeed;

    public bool Offset = false;
    public float TOffset = 0.1f;
    private void Update()
    {
        SplineUtility.GetNearestPoint(spline.Spline, Target.transform.position, out NearestPoint, out t);

        if (!Offset)
        {
            NearestPoint += (float3)spline.transform.position;
        }
        else
        {
            t += TOffset;
            NearestPoint = SplineUtility.EvaluatePosition(spline.Spline, t) + (float3)spline.transform.position;
        }

        transform.position = Vector3.Lerp(transform.position, NearestPoint, Time.deltaTime * FollowSpeed);

    }
}
