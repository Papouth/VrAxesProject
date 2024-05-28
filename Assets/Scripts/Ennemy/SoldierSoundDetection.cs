using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierSoundDetection : MonoBehaviour
{
    // Systeme de detection sonore des tirs du joueur

    #region Variables
    [SerializeField] private float detectRange;

    private Vector3 bulletPosition;
    private Vector3 soundPosition;

    private SoldierDetection soldierDetection;
    private NavMeshAgent agent;
    private SoldierLife soldierLifeScript;
    #endregion


    private void Start()
    {
        soldierLifeScript = GetComponent<SoldierLife>();
        agent = GetComponent<NavMeshAgent>();
        soldierDetection = GetComponent<SoldierDetection>();
    }

    private void Update()
    {
        if (IsShotFireDetected() && !soldierDetection.IsPlayerDetected() && soldierLifeScript.soldierLife > 0)
        {
            MoveAtSoundPos();
        }
    }

    private bool IsShotFireDetected()
    {
        if (GameObject.FindWithTag("ShotSound") != null)
        {
            soundPosition = GameObject.FindWithTag("ShotSound").transform.position;
        }

        if (soundPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, soundPosition) <= detectRange)
            {
                bulletPosition = soundPosition;
                return true;
            }
        }
        return false;
    }

    private void MoveAtSoundPos()
    {
        agent.isStopped = false;
        agent.SetDestination(bulletPosition);
    }
}