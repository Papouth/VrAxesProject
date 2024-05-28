using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public bool onFire; //  Si la mèche est en feu
    private bool alreadyExplode;
    private GameObject fizzle; // La mèche
    private DynamiteChecker checker;

    public GameObject explosion;
    public GameObject explosionBody;
    private MeshRenderer rend;


    private void Start()
    {
        onFire = false;
        alreadyExplode = false;

        // GameObject Fizzle Off
        fizzle = GetComponentInChildren<ParticleSystem>().gameObject;
        fizzle.SetActive(false);

        checker = GetComponentInChildren<DynamiteChecker>();
        checker.gameObject.SetActive(false);
        rend = GetComponent<MeshRenderer>();

        rend.enabled = true;
    }

    private void Update()
    {
        Bomb();
    }

    private void Bomb()
    {
        if (onFire && !alreadyExplode)
        {
            fizzle.SetActive(true);
            Invoke("CheckerExplode", 4f);
            Invoke("Explode", 5f);
            onFire = false;
            alreadyExplode = true;
        }
    }

    private void CheckerExplode()
    {
        checker.gameObject.SetActive(true);
    }

    private void Explode()
    {
        // Explosion
        if (!checker.body)
        {
            Debug.Log("Boom !");
            Instantiate(explosion, gameObject.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        else if (checker.body)
        {
            Debug.Log("Oh Shit !");
            Instantiate(explosionBody, gameObject.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }

        // On enlève le mesh renderer et la mèche
        rend.enabled = false;
        fizzle.SetActive(false);

        Invoke("Death", 5f);
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            onFire = true;
        }
    }
}