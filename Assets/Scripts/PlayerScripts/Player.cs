using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    public int playerLife = 100;

    [Header("Capsule Collider")]
    [SerializeField] private CapsuleCollider capsuleMove;
    [SerializeField] private CapsuleCollider capsuleDamage;

    [SerializeField] private Transform mainCam;
    #endregion


    private void Awake()
    {
        mainCam = GetComponentInChildren<Camera>().transform;
    }

    private void Start()
    {
        playerLife = 100;
    }

    public void Update()
    {
        Resizer();

        Die();
    }

    public void Die()
    {
        if (playerLife <= 0)
        {
            //Debug.Log("Vous êtes mort !");
        }
    }

    private void Resizer()
    {
        #region Capsule Collider
        // Capsule Move
        capsuleMove.height = mainCam.position.y - transform.position.y;
        capsuleMove.center = new Vector3(0, (mainCam.position.y / 2f) - (transform.position.y / 2f), 0);

        // Capsule Damage
        capsuleDamage.height = mainCam.position.y - transform.position.y;
        capsuleDamage.center = new Vector3(0, (mainCam.position.y / 2f) - (transform.position.y / 2f), 0);
        #endregion
    }
}