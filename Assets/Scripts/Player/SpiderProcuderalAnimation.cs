using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiderProcuderalAnimation : MonoBehaviour
{
    private SphereCollider SpiderCollider;

    public Transform[] LegsTargets;
    public Transform[] LegsDefaultPositions;
    public float legspeed;
    public float stepdistence;
    public AnimationCurve AnimationCurve,StepHightCurve;
    public LayerMask groundlayer;

    private Leg[] Legs;
    private bool ALegIsMoving;



    [Serializable]
    public struct Leg {
        public Transform Target;
        public Vector3 Oldpos;
        public Vector3 NextPos;
        public float DistenceFromDefault;
        public Transform DefaultPosition;
        public Transform IdlePosition;
    }




    private void Start()
    {
        SpiderCollider = GetComponent<SphereCollider>();

        Legs = new Leg[LegsTargets.Length];
        for (int i = 0; i < LegsTargets.Length; i++)
        { 
            Legs[i].NextPos = LegsTargets[i].transform.position;
            Legs[i].Target = LegsTargets[i].transform;
            Legs[i].DefaultPosition = LegsDefaultPositions[i].transform;
            Legs[i].Oldpos = LegsTargets[i].transform.position;
            Legs[i].IdlePosition = new GameObject().transform;
            Legs[i].IdlePosition.position = LegsDefaultPositions[i].transform.position;
            Legs[i].IdlePosition.transform.SetParent(LegsDefaultPositions[i].transform.parent);

        }
        ALegIsMoving = false;

    }
    private void Update()
    {
        if (MovmentController.Instance.Freeze)
        {
            for(int i = 0; i < Legs.Length; i++)
            {
                Legs[i].Target.position = LegsDefaultPositions[i].position;
            }

            return;
        }

        int farthestlegindex = 0;
        float maxdistence = 0;
        for (int i = 0; i < LegsTargets.Length; i++)
        {
            Legs[i].Target.position = Legs[i].NextPos;
            Legs[i].DistenceFromDefault = (float)Math.Round(Vector3.Distance(LegsTargets[i].position, LegsDefaultPositions[i].position),3);
            if (Legs[i].DistenceFromDefault > maxdistence)
            { 
                maxdistence = Legs[i].DistenceFromDefault;
                farthestlegindex = i;
            }
            RaycastHit hit;
            Ray ray = new Ray(Legs[i].IdlePosition.position + transform.up * 0.5f, -transform.up);

            if (Physics.Raycast(ray, out hit, 1f, groundlayer))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance,Color.red);
            LegsDefaultPositions[i].position = hit.point;
            }
        }
        if (!ALegIsMoving && Legs[farthestlegindex].DistenceFromDefault > stepdistence)
            StartCoroutine(IDoAStep(farthestlegindex));
        
    }

    IEnumerator IDoAStep(int legindex)
    {
       

        ALegIsMoving = true;
        float timer = 0;
        Vector3 MoveDirection = Legs[legindex].DefaultPosition.position - Legs[legindex].Oldpos;
        MoveDirection.y = 0;
        while (timer < (stepdistence/legspeed))
        {
            float t = AnimationCurve.Evaluate(timer / (stepdistence / legspeed));
            float h = StepHightCurve.Evaluate(timer / (stepdistence / legspeed));
            Vector3 targetposition = new(Vector3.Lerp(Legs[legindex].Oldpos, Legs[legindex].DefaultPosition.position + MoveDirection.normalized * stepdistence * 0.7f, t).x, Legs[legindex].DefaultPosition.position.y + h, Vector3.Lerp(Legs[legindex].Oldpos, Legs[legindex].DefaultPosition.position + MoveDirection.normalized * stepdistence * 0.7f, t).z);
            Legs[legindex].NextPos = targetposition;
            timer += Time.deltaTime;
            if (timer / (stepdistence / legspeed) > 0.75f) 
                ALegIsMoving = false;
            yield return null;
        }
        if(Math.Abs(GetComponent<Rigidbody>().velocity.y)  < 0.3f )
        AudioManager.Instance.PlaySound("footstep");

        yield return null;
        Legs[legindex].Oldpos = Legs[legindex].NextPos;
        ALegIsMoving =false;
    
        
    }


    public bool Grounded()
    {
        bool IsGrouned = false;
        Ray ray = new Ray(this.transform.position,Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, SpiderCollider.radius / 2, groundlayer))
            IsGrouned = true;
        else
            IsGrouned = false;

        
        return IsGrouned;
    }
}
