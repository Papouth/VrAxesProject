using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class M1911FireBullet : MonoBehaviour
{
    #region Variables
    private Animator shotAnim;
    public M1911Magazine magazine;
    public GameObject posMag;
    private M1911PosMag posMagScript;

    private Vector3 virtualUpRecoil;
    private Vector3 originalRotation;

    private Vector3 physicUpRecoil;
    private Vector3 pivotOriginalRot;
    public Transform recoilPivot;

    private bool lastShot;
    public static bool m1911IsHold;


    [Header("Bullet")]
    public Transform bulletSpawnPos;
    public GameObject bullet;
    public Transform douilleSpawnPos;
    public GameObject douille;

    private ParticleSystem psMuzzle;

    [Header("Silencer")]
    public GameObject silencerParent;
    public bool silencer;

    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip silencerSound;
    public AudioClip emptyFireSound;
    public AudioClip replaceMag;
    public AudioClip removeMag;

    private AudioSource m1911Audio;
    public GameObject shotSoundForIA;

    [Header("Culasse")]
    public UnityEvent CulasseRavance;
    #endregion


    private void Start()
    {
        m1911Audio = GetComponent<AudioSource>();

        silencer = false;
        shotAnim = GetComponent<Animator>();

        posMagScript = posMag.GetComponent<M1911PosMag>();

        psMuzzle = GetComponentInChildren<ParticleSystem>();

        originalRotation = transform.localEulerAngles;

        pivotOriginalRot = transform.localEulerAngles;

        lastShot = false;
    }

    public void GunHoldInHand()
    {
        m1911IsHold = true;
    }

    public void GunNotHoldInHand()
    {
        m1911IsHold = false;
    }

    public void Tir()
    {
        magazine = GetComponentInChildren<M1911Magazine>();

        // Si il y a un chargeur et qu'il n'est pas vide et qu'une balle est chambré
        if (posMag.transform.childCount > 0 && !magazine.emptyMag && posMagScript.chambrer)
        {
            if (!silencer)
            {
                m1911Audio.clip = fireSound;
                m1911Audio.Play();

                Instantiate(shotSoundForIA, gameObject.transform);

                // Muzzle Flash
                psMuzzle.Play();
            }
            else
            {
                m1911Audio.clip = silencerSound;
                m1911Audio.Play();
            }

            if (magazine.nextBullet < 6)
            {
                shotAnim.Play("Shot");
            }
            else
            {
                shotAnim.Play("LastShot");
            }

            magazine.DestroyBullet();

            lastShot = false;

            AddRecoil();

            // On instantie une balle
            Instantiate(bullet, bulletSpawnPos);

            // On instantie la douille
            Instantiate(douille, douilleSpawnPos.transform.position, douilleSpawnPos.transform.rotation, douilleSpawnPos);

            CulasseRavance.Invoke();
        }
        else if (posMag.transform.childCount > 0 && magazine.emptyMag)
        {
            EmptySound();
        }
        else if (posMag.transform.childCount > 0 && !magazine.emptyMag && !posMagScript.chambrer)
        {
            EmptySound();
        }
        
        if (posMag.transform.childCount == 0)
        {
            EmptySound();
        }
    }

    private void EmptySound()
    {
        // Bruit de tir à vide
        m1911Audio.clip = emptyFireSound;
        m1911Audio.Play();

        lastShot = true;
    }

    private void SilencerCheck()
    {
        if (silencerParent.transform.childCount > 0)
        {
            silencer = true;
        }
        else
        {
            silencer = false;
        }
    }

    private void Update()
    {
        SilencerCheck();
        RecoilCalculator();
    }

    private void RecoilCalculator()
    {
        float recoil = Random.Range(2f, 6f);

        virtualUpRecoil = new Vector3(recoil, 0f, 0f);
        physicUpRecoil = new Vector3(0f, recoil, 0f);
    }

    private void AddRecoil()
    {
        // Recul de la "balle"
        originalRotation = transform.localEulerAngles;
        transform.localEulerAngles += virtualUpRecoil;


        // Arme recul
        pivotOriginalRot = transform.localEulerAngles;
        recoilPivot.localEulerAngles -= physicUpRecoil; 
    }

    public void StopRecoil()
    {
        // Si chargeur pas vide alors on reset le recul
        if (!lastShot)
        {
            transform.localEulerAngles = originalRotation;


            recoilPivot.localRotation = Quaternion.Euler(0f, -82f, -90f);
        }
    }
}