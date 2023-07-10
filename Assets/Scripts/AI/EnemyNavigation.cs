using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNavigation : MonoBehaviour
{
    private NavMeshAgent _enemyAgent;
    private FieldOfView _fieldOfView;
    private Animator _enemyAnimator;
    public Transform[] WayPoints;
    private bool FreeRoming;

    [Header("Navigation")]
    public float MoveSpeed = 3;
    public float IdleTime = 5;
    private int Direction;
    public int CurrentPoint;

    [Header("Player Detection")]
    private PanicLevel _panicLevel;
    public float EndGameTime = 3;
    public float EndGameTimer;




    private void Start()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();
        _fieldOfView = GetComponent<FieldOfView>();
        _panicLevel = GetComponent<PanicLevel>();
        _enemyAnimator = GetComponent<Animator>();

        transform.position = WayPoints[0].position;

        EndGameTimer = EndGameTime;

        if(!FreeRoming)
            FirstPoint();
    }

    private IEnumerator NextPointEnumerator;
    private void Update()
    {
        if (!_fieldOfView.IsTargetVisible())
        {
            if (EndGameTimer < EndGameTime)
            {
                 EndGameTimer += Time.deltaTime;
            }

            if (EndGameTimer >= EndGameTime)
            {
                EndGameTimer = EndGameTime;

                if (!_enemyAgent.enabled)
                {
                    _enemyAgent.enabled = true;
                    _enemyAgent.SetDestination(WayPoints[CurrentPoint].position);
                    Idle = false;
                }

                if (_enemyAgent.remainingDistance< _enemyAgent.stoppingDistance && !Idle)
                {
                    NextPointEnumerator = NextPoint();
                    StartCoroutine(NextPointEnumerator);
                }
                //Animate

                if (_enemyAgent.velocity != Vector3.zero)
                    _enemyAnimator.SetBool("Walk",true);
                else
                    _enemyAnimator.SetBool("Walk", false);
            }
        }
        else
        {
            _enemyAgent.enabled = false;
            StopCoroutine(NextPointEnumerator);
            Vector3 Direction = MovmentController.Instance.transform.position - transform.position;
            Direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation((Direction).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.8f * Time.deltaTime);
            EndGameTimer -= Time.deltaTime;


            if (EndGameTimer < 0)
                print("Game Over");

            _enemyAnimator.SetBool("Walk", false);
        }


        if (EndGameTimer < EndGameTime)
            _panicLevel.targetPanicLevel.gameObject.SetActive(true);
        else
            _panicLevel.targetPanicLevel.gameObject.SetActive(false);
    }

    /*
    private void NextPoint()
    {
        _enemyAgent.isStopped = false;

        if (CurrentPoint >= WayPoints.Length - 1 || CurrentPoint <= 0) //Inverse Direction
        {
            Direction = -Direction;
        }

        CurrentPoint += Direction;

        print(CurrentPoint);
        _enemyAgent.SetDestination(WayPoints[CurrentPoint].position);
    }
    */

    private void FirstPoint()
    {
        _enemyAgent.isStopped = false;
        Direction = 1;
        CurrentPoint = 1;

        _enemyAgent.SetDestination(WayPoints[CurrentPoint].position);
    }

    private bool Idle;
    public IEnumerator NextPoint()
    {
        Idle = true;
        yield return new WaitForSeconds(IdleTime);
        _enemyAgent.isStopped = false;

        if (FreeRoming)
        {
            if (CurrentPoint >= WayPoints.Length - 1 || CurrentPoint <= 0) //Inverse Direction
            {
                Direction = -Direction;
            }

            CurrentPoint += Direction;

            _enemyAgent.SetDestination(WayPoints[CurrentPoint].position);
        }
        else
        {
            CurrentPoint++;
            if (CurrentPoint < WayPoints.Length - 1) //Inverse Direction
            {
                _enemyAgent.SetDestination(WayPoints[CurrentPoint].position);
            }
            else
                Checking = false;
        }


        Idle = false;
    }

    private bool Checking;
    public void Alerted()
    {
        if (!Checking)
        {
            Checking = true;
            _enemyAgent.isStopped = false;
            Direction = 1;
            CurrentPoint = 1;

            _enemyAgent.SetDestination(WayPoints[CurrentPoint].position);
        }
    }
}
