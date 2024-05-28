using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911Douille : MonoBehaviour
{
    private Rigidbody rbDouille;

    void Start()
    {
        rbDouille = GetComponent<Rigidbody>();

        rbDouille.AddForce(transform.forward * 4f, ForceMode.Impulse);
        var random = Random.Range(-90, 90);
        var randomTorque = new Vector3(random, random, random);
        rbDouille.AddTorque(randomTorque, ForceMode.Impulse);
    }
}