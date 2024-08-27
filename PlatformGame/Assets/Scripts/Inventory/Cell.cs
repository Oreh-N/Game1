using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Inventory inventory;
    public int id;


    void Start()
    { inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>(); }


    private void Update()
    {
        if (transform.childCount <= 0)
        { inventory.isFull[id] = false; }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            if (inventory.isFull[id])
            {
                child.GetComponent<Spawn>().SpawnDroppedItem();
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void UseItem()
    {
        foreach (Transform child in transform)
        {
            if (inventory.isFull[id] && child.GetComponent<Healing>() != null)
            {
                child.GetComponent<Healing>().Use();
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
