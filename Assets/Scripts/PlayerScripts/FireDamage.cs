using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private bool onFire;
    [SerializeField] private Player player;
    private float time;
    private float contactTime;


    private void Start()
    {
        player = GetComponentInParent<Player>();
        time = 1.5f;
        contactTime = 0f;
    }

    private void Update()
    {
        if (onFire)
        {
            contactTime += Time.deltaTime;

            if (time < contactTime)
            {
                player.playerLife -= 5;
                contactTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire")) onFire = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire")) onFire = false;
    }
}