using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierMovement : MonoBehaviour
{
    // Système de navigation A*

    #region Variables
    [Header("Navigation Parameters")]
    //[SerializeField] private float rotationSpeed = 5f;
    //[SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTimer = 2f;

    public Transform[] waypoints;
    [SerializeField] private int currentWaypoint;
    private Vector3 wantedPos;
    private int crease;

    private Transform target;
    private float timer;
    private NavMeshAgent agent;
    private SoldierDetection soldierDetection;
    private Animator anim;
    private SoldierLife soldierLifeScript;
    #endregion


    private void Start()
    {
        soldierLifeScript = GetComponent<SoldierLife>();
        timer = wanderTimer;
        agent = GetComponent<NavMeshAgent>();
        soldierDetection = GetComponent<SoldierDetection>();
        anim = GetComponentInChildren<Animator>();

        agent.destination = waypoints[currentWaypoint].position;
    }

    private void Update()
    {
        if (!soldierDetection.IsPlayerDetected() && soldierLifeScript.soldierLife > 0)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                /*
                // RandomNavSphere
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);

                target = new GameObject().transform;
                target.position = newPos;
                agent.SetDestination(newPos);

                // Rotation
                Quaternion newRotation = Quaternion.LookRotation(target.position - transform.position);
                agent.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

                timer = 0;
                Destroy(target.gameObject);*/


                Waypoint();
            }
        }

        if (agent.velocity == Vector3.zero)
        {
            // Animation D'IDLE
            anim.SetInteger("Status_walk", 0);
        }
        else
        {
            // Animation De Marche
            anim.SetInteger("Status_walk", 1);
        }

        if (currentWaypoint == 0) crease = 1;
        else if (currentWaypoint == waypoints.Length)
        {
            currentWaypoint--;
            crease = -1;
        }
    }

    private void Waypoint()
    {
        // Waypoints
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            wantedPos = waypoints[currentWaypoint].position;
            agent.isStopped = false;
            agent.SetDestination(wantedPos);

            currentWaypoint += crease;

            timer = 0;
        }
    }

    /*
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
    */
}