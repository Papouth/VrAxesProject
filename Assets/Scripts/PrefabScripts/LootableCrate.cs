using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootableCrate : MonoBehaviour
{
    #region Variables
    [Header("Random Item")]
    [Tooltip("Là ou spawn mes items")]
    public Transform itemSpawnPos;

    [Tooltip("Items commun")]
    public GameObject[] spawnableItemsCommon;
    [Tooltip("Items rare")]
    public GameObject[] spawnableItemsRare;
    [Tooltip("Items épique")]
    public GameObject[] spawnableItemsEpic;

    private int itemRarity;
    private int itemNumber;

    [Header("Loot UI")]
    public TextMeshProUGUI rarityText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI abilityText;

    [SerializeField] private Color commonColor = new Color(68, 210, 47);
    [SerializeField] private Color rareColor = new Color(224, 99, 0);
    [SerializeField] private Color epicColor = new Color(159, 40, 181);

    [SerializeField] private GameObject hatInside;
    private Hat hatScript;
    #endregion


    private void Start()
    {
        // On défini l'objet qui sera dans la caisse
        RandomItem();
    }

    private void RandomItem()
    {
        itemRarity = Random.Range(1, 20);

        if (itemRarity <= 10)
        {
            // Item Commun
            rarityText.color = commonColor;

            rarityText.text = "*";

            itemNumber = Random.Range(0, spawnableItemsCommon.Length);

            hatInside = Instantiate(spawnableItemsCommon[itemNumber], itemSpawnPos.position, Quaternion.identity);

            // Hat UI Display
            hatScript = hatInside.GetComponent<Hat>();
            nameText.text = hatScript.hatName;
            abilityText.text = hatScript.hatAbility;
        }
        else if (itemRarity > 10 && itemRarity <= 15)
        {
            // Item Rare
            rarityText.color = rareColor;

            rarityText.text = "**";

            itemNumber = Random.Range(0, spawnableItemsRare.Length);

            hatInside = Instantiate(spawnableItemsRare[itemNumber], itemSpawnPos.position, Quaternion.identity);

            // Hat UI Display
            hatScript = hatInside.GetComponent<Hat>();
            nameText.text = hatScript.hatName;
            abilityText.text = hatScript.hatAbility;
        }
        else if (itemRarity > 15)
        {
            // Item Épique
            rarityText.color = epicColor;

            rarityText.text = "***";

            itemNumber = Random.Range(0, spawnableItemsEpic.Length);

            hatInside = Instantiate(spawnableItemsEpic[itemNumber], itemSpawnPos.position, Quaternion.identity);

            // Hat UI Display
            hatScript = hatInside.GetComponent<Hat>();
            nameText.text = hatScript.hatName;
            abilityText.text = hatScript.hatAbility;
        }
    }
}