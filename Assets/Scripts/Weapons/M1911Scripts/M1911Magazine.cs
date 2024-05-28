using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911Magazine : MonoBehaviour
{
    public Transform[] bulletsTransform;
    public int nextBullet;
    public bool emptyMag;

    private void Start()
    {
        emptyMag = false;
        nextBullet = 0;

        bulletsTransform = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            bulletsTransform[i] = transform.GetChild(i);
        }
    }

    public void DestroyBullet()
    {
        if (nextBullet < 7)
        {
            Destroy(bulletsTransform[nextBullet].gameObject);

            nextBullet++;
        }

        if (nextBullet == 7)
        {
            // Le chargeur est vide
            emptyMag = true;
        }
    }
}