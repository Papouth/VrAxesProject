using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierLife : MonoBehaviour
{
    // Système pour changer d'animation d'arme (viser, recharger, idle = tirer)

    #region Variables
    [Header("Ennemy Parameters")]
    public int soldierLife;
    public GameObject weapon;

    [Header("Ennemy Component")]
    private Animator animator;
    private Rigidbody[] ragdollBodies;
    #endregion

    public void Start()
    {
        soldierLife = 100;

        animator = GetComponentInChildren<Animator>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>();

        ToggleRagdoll(false);
        weapon.SetActive(true);
    }

    public void Update()
    {
        Die();
    }

    private void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }
    }

    public void Die()
    {
        if (soldierLife <= 0)
        {
            ToggleRagdoll(true);

            foreach (Rigidbody rb in ragdollBodies)
            {
                rb.AddExplosionForce(107f, new Vector3(-1f, 0.5f, -1f), 5f, 0f, ForceMode.Impulse);
            }

            weapon.SetActive(false);
        }
    }
}