using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinePouch : MonoBehaviour
{
    [SerializeField] private GameObject m1911Mag;
    [SerializeField] private GameObject currentMag;
    [SerializeField] private Transform[] bulletsTransform;
    [SerializeField] private GameObject clonedMag;
    private bool inPouch;
    private bool security;

    private PlayerInput playerInput;



    private void Update()
    {
        DetectCurrentWeapon();

        NewMag();

        MagOn();

        MagOnSecurity();
    }

    private void NewMag()
    {
        if (clonedMag == null && currentMag != null)
        {
            clonedMag = Instantiate(currentMag, transform.position, Quaternion.identity);

            clonedMag.GetComponent<MeshRenderer>().enabled = false;

            bulletsTransform = new Transform[clonedMag.transform.childCount];

            for (int i = 0; i < clonedMag.transform.childCount - 1; i++)
            {
                bulletsTransform[i] = clonedMag.transform.GetChild(i);
                bulletsTransform[i].GetComponent<MeshRenderer>().enabled = false;
            }
            
            return;
        }
    }

    private void DetectCurrentWeapon()
    {
        if (M1911FireBullet.m1911IsHold)
        {
            // Destruction de l'ancien chargeur
            if (currentMag != m1911Mag)
            {
                Destroy(currentMag);

                // Attribution du nouveau chargeur
                currentMag = m1911Mag;
            }
        }
    }

    public void MagOn()
    {
        if (inPouch && playerInput.isGripped && clonedMag != null && !security)
        {
            clonedMag.GetComponent<MeshRenderer>().enabled = true;

            bulletsTransform = new Transform[clonedMag.transform.childCount];

            for (int i = 0; i < clonedMag.transform.childCount - 1; i++)
            {
                bulletsTransform[i] = clonedMag.transform.GetChild(i);
                bulletsTransform[i].GetComponent<MeshRenderer>().enabled = true;
            }

            clonedMag = null;
            inPouch = false;

            security = true;
        }
    }

    private void MagOnSecurity()
    {
        if (inPouch)
        {
            if (!playerInput.isGripped && security)
            {
                security = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandTag"))
        {
            inPouch = true;
            playerInput = other.GetComponentInParent<PlayerInput>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandTag")) inPouch = false;
    }
}