using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float rushSpeed = 10f;
    public float rotationSpeed = 5f;
    public Transform[] waypoints;
    public float defaultMovementInterval = 5f;
    public float hideTime = 5f;

    private NavMeshAgent agent;
    private FieldOfView fieldOfView;
    private bool isRushing = false;
    private Vector3 lastKnownPlayerPosition;
    private float lastKnownPlayerTime = 0f;
    private float currentHideTime = 0f;
    private int currentWaypointIndex = 0;
    private int waypointDirection = 1;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<FieldOfView>();
    }

    private void Start()
    {
        SetNextWaypoint();
        InvokeRepeating("MoveToNextWaypoint", 2f, defaultMovementInterval);
    }

    private void Update()
    {
        if (fieldOfView.IsTargetVisible())
        {

            lastKnownPlayerPosition = player.position;

            lastKnownPlayerTime = Time.time;

            
            RushToPlayer();
        }
        else if (Time.time - lastKnownPlayerTime <= hideTime)
        {



            agent.SetDestination(lastKnownPlayerPosition);
            currentHideTime = Time.time;
        }
        else if (isRushing)
        {
            ResumeDefaultMovement();
        }
    }

    private void RushToPlayer()
    {
        if (!isRushing)
        {

            isRushing = true;
            agent.speed = rushSpeed;
            agent.SetDestination(player.position);
        }
    }

    private void ResumeDefaultMovement()
    {
        if (isRushing)
        {
            // Stop rushing and resume default movement
            isRushing = false;
            agent.speed = 3f;
            SetNextWaypoint();
        }
    }

    private void MoveToNextWaypoint()
    {

                    Debug.Log(message:"index :"  +  currentWaypointIndex.ToString());
        if (!isRushing && waypoints.Length > 0)
        {


            agent.SetDestination(target: waypoints[currentWaypointIndex].position);
            currentWaypointIndex += waypointDirection;

            if (currentWaypointIndex >= waypoints.Length || currentWaypointIndex < 0)
            {
                // Reverse the waypoint direction
                waypointDirection *= -1;
                currentWaypointIndex += waypointDirection;
            }
        }
    }

    private void SetNextWaypoint()
    {
        if (waypoints.Length > 0)
        {
            currentWaypointIndex = (waypointDirection == 1) ? 0 : waypoints.Length - 1;

            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
