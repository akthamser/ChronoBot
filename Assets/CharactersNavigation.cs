using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharactersNavigation : MonoBehaviour
{
    private NavMeshAgent _enemyAgent;
    private FieldOfView _fieldOfView;
    private Animator _enemyAnimator;
    public Transform StartPoint;
    public Transform TargetPoint;
    public float EndAlertTime = 3;
    public float StartAlertTime = 4;


    private void Start()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();
        _fieldOfView = GetComponent<FieldOfView>();
        _enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (!_fieldOfView.IsTargetVisible())
        {
            _enemyAgent.enabled = true;
            if (Alerted)
                _enemyAgent.SetDestination(TargetPoint.position);
            else
                _enemyAgent.SetDestination(StartPoint.position);


            if (Alerted && Vector3.Distance(this.transform.position,TargetPoint.position) < _enemyAgent.stoppingDistance)
                StartCoroutine(EndAlert());
        }
        else
        {
            _enemyAgent.enabled = false;
            MovmentController.Instance.Spoted();
        }

       
        //Animate
        if(_enemyAgent.velocity != Vector3.zero)
        {
            _enemyAnimator.SetBool("Walk", true);
            AudioManager.Instance.PlaySound("guardwalk");
            soundon = true;
        }
        else
        {
            _enemyAnimator.SetBool("Walk", false);

            if (soundon)
            {
            AudioManager.Instance.StopSound("guardwalk",0.4f);
                soundon=false;
            }
        }
            
            

    }
    private bool soundon = false;
    public bool Alerted = false;
    public void Alert()
    {
        StartCoroutine(StartAlert());
    }

    public IEnumerator StartAlert()
    {
        yield return new WaitForSeconds(StartAlertTime);
        Alerted = true;
    }
    public IEnumerator EndAlert()
    {
        yield return new WaitForSeconds(EndAlertTime);
        Alerted = false;
    }
}
