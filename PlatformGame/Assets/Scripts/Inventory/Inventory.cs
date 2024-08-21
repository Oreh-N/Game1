using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] cells;
    public GameObject inventory;
    private bool inventoryOn;

    private void Start()
    { inventoryOn = false; }

    public void Bag()
    {
        if (!inventoryOn)
        {
            inventoryOn = true;
            inventory.SetActive(true);
        }
        else if (inventoryOn)
        {
            inventoryOn = false;
            inventory.SetActive(false);
        }
    }
}
