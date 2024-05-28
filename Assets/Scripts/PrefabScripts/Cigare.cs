using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigare : MonoBehaviour
{
    private bool onFire;
    private GameObject smokeFX;



    private void Start()
    {
        smokeFX = GetComponentInChildren<ParticleSystem>().gameObject;
        smokeFX.SetActive(false);
    }

    private void Update()
    {
        Smoking();
    }

    private void Smoking()
    {
        if (onFire) smokeFX.SetActive(true);
        else if (!onFire) smokeFX.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire")) onFire = true;

        if (other.CompareTag("Wall")) onFire = false;
    }
}