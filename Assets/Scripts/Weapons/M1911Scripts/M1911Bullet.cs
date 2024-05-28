using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911Bullet : MonoBehaviour
{
    #region Variables
    private SoldierLife soldier;
    private Player player;

    [Header("VFX Bullet Impact")]
    public GameObject blood;
    public GameObject dust;

    [Header("Component")]
    private Rigidbody rbBullet;
    #endregion


    private void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        Invoke("BulletDie", 5f);
    }

    private void FixedUpdate()
    {
        rbBullet.AddForce(transform.forward * 12f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Soldier"))
        {
            soldier = collision.gameObject.GetComponentInParent<SoldierLife>();
            soldier.soldierLife -= Random.Range(28, 42);

            ContactPoint contact = collision.GetContact(0);

            blood.transform.position = contact.point;
            blood.transform.forward = contact.normal;

            Instantiate(blood);

            BulletDie();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            ContactPoint contact = collision.GetContact(0);

            dust.transform.position = contact.point;
            dust.transform.forward = contact.normal;

            Instantiate(dust);

            BulletDie();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            player.playerLife -= Random.Range(28, 42);

            ContactPoint contact = collision.GetContact(0);

            blood.transform.position = contact.point;
            blood.transform.forward = contact.normal;

            Instantiate(blood);

            BulletDie();
        }
    }

    private void BulletDie()
    {
        Destroy(gameObject);
    }
}