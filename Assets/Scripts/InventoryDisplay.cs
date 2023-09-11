using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Inventory inventory;
    private Text inventoryText;

    private void Awake()
    {
        inventoryText = GetComponent<Text>();
    }

    private void Update()
    {
        inventoryText.text = "Inventory:\n";
        foreach (string item in inventory.collectedObjects)
        {
            inventoryText.text += item + "\n";
        }
    }
}