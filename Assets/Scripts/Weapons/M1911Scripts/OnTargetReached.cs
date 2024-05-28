using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTargetReached : MonoBehaviour
{
    private bool wasReached;
    public float threshold = 0.02f;
    public Transform target;
    public UnityEvent OnReached;

    public GameObject rotationMarteau;


    public void Start()
    {
        wasReached = false;
    }

    public void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < threshold && !wasReached)
        {
            OnReached.Invoke();
            wasReached = true;

            // Rotation du marteau à 0
            rotationMarteau.transform.localRotation = Quaternion.identity;
        }
        else if (distance >= threshold)
        {
            wasReached = false;
        }
    }
}