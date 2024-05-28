using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class M1911PosMag : MonoBehaviour
{
    [SerializeField] private InputActionReference secondaryButtonBReference;
    [SerializeField] private InputActionReference secondaryButtonYReference;

    XRGrabInteractable m_GrabInteractable;

    private Rigidbody magRb;
    private GameObject tempMag;
    private M1911Magazine magScript;
    private MeshCollider magCol;

    private bool magInside;

    private Animator m1911Animator;

    // Est ce qu'une balle est chambré ?
    public bool chambrer;
    public bool culasseArriere;

    [Header("Audio")]
    public AudioClip replaceMag;
    public AudioClip removeMag;
    public AudioClip culasseSlide;
    public AudioClip balleChambrer;

    private AudioSource magAudio;


    private void Start()
    {
        m1911Animator = GetComponentInParent<Animator>();

        secondaryButtonBReference.action.performed += OnSecondaryB;
        secondaryButtonYReference.action.performed += OnSecondaryY;

        magAudio = GetComponent<AudioSource>();

        chambrer = false;
        culasseArriere = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mag") && !magInside)
        {
            magRb = other.GetComponent<Rigidbody>();
            tempMag = other.gameObject;
            m_GrabInteractable = tempMag.GetComponent<XRGrabInteractable>();
            magCol = tempMag.GetComponent<MeshCollider>();

            magScript = tempMag.GetComponent<M1911Magazine>();


            // On désactive le collider
            magCol.enabled = false;

            // Sound
            magAudio.clip = replaceMag;
            magAudio.Play();


            // Désactivation de la possibilité de grab l'objet
            m_GrabInteractable.enabled = false;


            // Le chargeur n'est plus soumis à la gravité
            magRb.useGravity = false;
            magRb.isKinematic = true;


            // Magazine Position devient le parent du chargeur et on reset la position du chargeur
            tempMag.transform.parent = gameObject.transform;
            tempMag.transform.localPosition = new Vector3(0f, 0f, 0f);
            tempMag.transform.localRotation = Quaternion.identity;

            magInside = true;
            chambrer = false;
        }
    }

    public void EjectMag()
    {
        // Éjection du chargeur du pistolet

        // On vérifie s'il y a un chargeur
        if (magRb != null)
        {
            // On retire le parent du chargeur
            tempMag.transform.parent = null;


            gameObject.GetComponent<SphereCollider>().enabled = false;

            // Sound
            magAudio.clip = removeMag;
            magAudio.Play();


            // Réactivation de la possibilité de grab l'objet
            m_GrabInteractable.enabled = true;


            // On remet la gravité au chargeur
            magRb.isKinematic = false;
            magRb.useGravity = true;

            // On Réactive le collider
            magCol.enabled = true;

            // On reset la variable RigidBody
            magRb = null;

            magInside = false;

            Invoke("ColliderEnable", 0.4f);
        }
    }

    private void ColliderEnable()
    {
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    private void OnSecondaryB(InputAction.CallbackContext obj)
    {
        // Right Hand

        // Si la culasse n'est pas en arrière, j'éjecte mon chargeur ou si mon chargeur est vide, je l'éjecte d'abord
        if (!culasseArriere)
        {
            EjectMag();
        }
        else if (magScript.nextBullet == 7 && magInside)
        {
            EjectMag();
        }
        else if (culasseArriere)
        {
            CulasseAvant();
        }
        else if (magScript.nextBullet == 7 && !magInside)
        {
            CulasseAvant();
        }
    }

    private void OnSecondaryY(InputAction.CallbackContext obj)
    {
        // Left Hand

        // Si la culasse n'est pas en arrière, j'éjecte mon chargeur ou si mon chargeur est vide, je l'éjecte d'abord
        if (!culasseArriere)
        {
            EjectMag();
        }
        else if (magScript.nextBullet == 7 && magInside)
        {
            EjectMag();
        }
        else if (culasseArriere)
        {
            CulasseAvant();
        }
        else if (magScript.nextBullet == 7 && !magInside)
        {
            CulasseAvant();
        }
    }

    public void CulasseAvant()
    {
        culasseArriere = false;

        // On repousse la culasse vers l'avant
        m1911Animator.Play("CulasseRAZ");

        // Sound
        magAudio.clip = culasseSlide;
        magAudio.Play();


        //magScript = tempMag.GetComponent<M1911Magazine>();

        if (magScript.nextBullet < 7)
        {
            // S'il y a au moins une balle dans mon chargeur, alors je chambre la balle
            chambrer = true;
        }
    }

    public void ChambrageBalle()
    {
        // Si pas de balle de chambrer alors on en chambre une, si déjà une de chambrer alors, on enlève la précédente en la faisant sortir
        if (!chambrer)
        {
            // La culasse n'est plus vers l'arrière
            culasseArriere = false;

            // Une balle se trouve dans la chambre
            chambrer = true;

            // Sound
            magAudio.clip = balleChambrer;
            magAudio.Play();
        }
        else
        {
            // Si déjà une de chambrer alors, on enlève la précédente en la faisant sortir


            // Sound
            magAudio.clip = balleChambrer;
            magAudio.Play();
        }
    }

    public void CulasseBool()
    {
        culasseArriere = true;
    }
}