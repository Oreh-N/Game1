using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private PlayerController player;
    private Inventory inventory;
    public GameObject cellButton;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inventory = player.GetComponent<Inventory>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.cells.Length; i++)
            {
                if (gameObject.CompareTag("Coin"))
                { 
                    player.UpdateCoinCount(50);
                    Destroy(gameObject);
                    break;
                }

                else if (!inventory.isFull[i])
                {
                    inventory.isFull[i] = true;
                    Instantiate(cellButton, inventory.cells[i].transform);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
