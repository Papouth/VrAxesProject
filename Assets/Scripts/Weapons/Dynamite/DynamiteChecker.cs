using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteChecker : MonoBehaviour
{
    public bool body;

    private void Start()
    {
        body = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<SoldierLife>())
        {
            body = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<SoldierLife>())
        {
            body = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<SoldierLife>())
        {
            body = false;
        }
    }
}