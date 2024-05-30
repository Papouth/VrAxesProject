using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public int[] situations;

    public int pointsJoueur;
    [SerializeField] private float timer;
    private float timerReset;
    #endregion


    private void Start()
    {
        Time.timeScale = 1;

        timerReset = timer;
    }


    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Fin de la partie
            Time.timeScale = 0;
        }
    }

    public void Replay()
    {
        Time.timeScale = 1;
        timer = timerReset;
    }
}