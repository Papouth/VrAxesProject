using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierDetection : MonoBehaviour
{
    // Système de détection visuel du joueur

    #region Variables
    [Tooltip("La distance de détection")]
    [SerializeField] private float detectRange = 10f;

    [Tooltip("La distance à laquelle on peut tirer")]
    [SerializeField] private float stopDistance;

    [Tooltip("La fréquence de tir")]
    [SerializeField] private float fireRate = 1f;

    [Tooltip("D'où je tire le raycast de détection")]
    public Transform ennemyBone;

    [Tooltip("La balle qui va être tiré")]
    public GameObject bulletPrefab;

    [Tooltip("Là où la balle est tiré")]
    public Transform firePoint;

    [Tooltip("Le nombre de balle dans le chargeur")]
    [SerializeField] private int bulletsInMag = 30;
    private int baseMag;
    private bool alreadyReload;

    [Tooltip("Le son que va produire l'arme à chaque tir")]
    public AudioClip fireSound;
    private AudioSource source;

    [Tooltip("Le son émis lors du rechargement de l'arme")]
    public AudioClip reloadSound;

    [Tooltip("Mon joueur")]
    public Transform player;

    private float fireTimer = 0f;
    private float distanceToPlayer;
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] private string weaponStatut;
    private SoldierLife soldierLifeScript;
    #endregion



    private void Start()
    {
        soldierLifeScript = GetComponent<SoldierLife>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        baseMag = bulletsInMag;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (IsPlayerDetected() && soldierLifeScript.soldierLife > 0)
        {
            ChaseAndFire();
        }
    }

    public bool IsPlayerDetected()
    {
        RaycastHit hit;

        if (Physics.Raycast(ennemyBone.position, transform.forward, out hit, detectRange))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return false;
            }
            else if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void ChaseAndFire()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            // Si mon joueur est trop loin de l'IA, elle se rapproche de lui
            anim.SetInteger(weaponStatut, 0);

            agent.isStopped = false;
            agent.SetDestination(player.position);

            transform.LookAt(player);
        }
        else
        {
            // Si mon joueur est assé proche de l'IA, elle lui tire dessus
            agent.isStopped = true;

            transform.LookAt(player);
            fireTimer += Time.deltaTime;

            if (fireTimer >= fireRate && bulletsInMag > 0)
            {
                anim.SetInteger(weaponStatut, 1);
                source.PlayOneShot(fireSound);

                Instantiate(bulletPrefab, firePoint);
                bulletsInMag--;
                fireTimer = 0f;
            }
            else if (bulletsInMag == 0)
            {
                // On recharge
                anim.SetInteger(weaponStatut, 2);

                if (!alreadyReload)
                {
                    alreadyReload = true;
                    Invoke("ReloadGun", 1.4f);
                }
            }
        }
    }

    private void ReloadGun()
    {
        source.PlayOneShot(reloadSound);
        bulletsInMag = baseMag;
        anim.SetInteger(weaponStatut, 1);
        alreadyReload = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ennemyBone.position, transform.forward * detectRange);
    }
}