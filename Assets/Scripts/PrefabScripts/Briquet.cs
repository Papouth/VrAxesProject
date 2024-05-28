using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Briquet : MonoBehaviour
{
    private GameObject fireFX;


    private void Start()
    {
        fireFX = GetComponentInChildren<ParticleSystem>().gameObject;
        fireFX.SetActive(false);
    }

    public void TriggerLighterOn()
    {
        fireFX.SetActive(true);
    }

    public void TriggerLighterOff()
    {
        fireFX.SetActive(false);
    }
}