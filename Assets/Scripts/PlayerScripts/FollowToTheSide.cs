using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToTheSide : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        transform.position = target.position + Vector3.up * offset.y 
            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x 
            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;

        transform.eulerAngles = new Vector3(-3, 180 + target.eulerAngles.y, 0);
    }
}