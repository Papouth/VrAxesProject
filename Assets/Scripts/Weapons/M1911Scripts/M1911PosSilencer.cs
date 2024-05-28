using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class M1911PosSilencer : MonoBehaviour
{
    private Rigidbody silencerRb;
    private GameObject tempSilencer;
    private SphereCollider posCollider;

    private bool isAttach;

    XRGrabInteractable m_GrabInteractable;

    [Header("Sound")]
    public AudioClip clickSilencer;

    private AudioSource silencerAudio;
    private bool haveEmited;



    private void Start()
    {
        posCollider = GetComponent<SphereCollider>();
        posCollider.enabled = true;

        silencerAudio = GetComponent<AudioSource>();
        isAttach = false;
        haveEmited = false;
    }

    private void Update()
    {
        SoundParam();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Silencer"))
        {
            silencerRb = other.GetComponent<Rigidbody>();
            tempSilencer = other.gameObject;
            m_GrabInteractable = tempSilencer.GetComponent<XRGrabInteractable>();

            // Désactivation de la possibilité de grab l'objet
            m_GrabInteractable.enabled = false;


            // Le silencieux n'est plus soumis à la gravité
            silencerRb.useGravity = false;
            silencerRb.isKinematic = true;


            // Silencer Position devient le parent du silencieux et on reset la position du silencieux
            tempSilencer.transform.parent = gameObject.transform;
            tempSilencer.transform.localPosition = new Vector3(0f, 0f, 0f);
            tempSilencer.transform.localRotation = Quaternion.identity;


            // Possibilité de regrab le silencieux après une seconde
            Invoke("GrabParam", 1f);
            Invoke("AttachParam", 2.5f);
        }
    }

    private void SoundParam()
    {
        if (gameObject.transform.childCount > 0 && !haveEmited)
        {
            haveEmited = true;

            // Sound
            silencerAudio.clip = clickSilencer;
            silencerAudio.Play();
        }
        else if (gameObject.transform.childCount == 0)
        {
            haveEmited = false;
        }
    }

    private void GrabParam()
    {
        m_GrabInteractable.enabled = true;
    }

    private void AttachParam()
    {
        isAttach = true;
    }

    public void RemoveSilencer()
    {
        // On vérifie s'il y a un silencieux
        if (silencerRb != null && isAttach)
        {
            isAttach = false;

            // Désactivation point d'accroche
            posCollider.enabled = false;

            // On retire le parent du silencieux
            tempSilencer.transform.parent = null;


            // On remet la gravité au silencieux
            silencerRb.isKinematic = false;
            silencerRb.useGravity = true;


            // On reset la variable RigidBody
            silencerRb = null;

            // On peut replacer le silencieux
            Invoke("PosColliderParam", 2.5f);
        }
    }

    private void PosColliderParam()
    {
        // Réactivation du point d'accroche 
        posCollider.enabled = true;
    }
}