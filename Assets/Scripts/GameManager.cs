using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Spawn LootCrates")]
    public GameObject lootCrate;

    [Tooltip("L'endroit où va spawn ma caisse sur la map")]
    public Transform[] spawnLootCratePos;
    #endregion

    private void Start()
    {
        SpawnLootCrate();
    }

    private void SpawnLootCrate()
    {

    }
}