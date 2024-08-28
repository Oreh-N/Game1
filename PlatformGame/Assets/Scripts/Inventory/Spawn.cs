using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public LayerMask whatIsObstacle;
    private Transform player;
    public GameObject item;
    float rayLength = 0.1f;


    private void Start()
    { player = GameObject.FindGameObjectWithTag("Player").transform; }

    public void SpawnInventoryItem()
    {
        float xDist = 1, yDist = -0.5f;
        try
        {
            while (true)
            {
                Vector2 spawnPos = new Vector2(player.position.x + xDist, player.position.y - yDist);
                RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, rayLength, whatIsObstacle);

                if (hit.collider == null)
                { 
                    Instantiate(item, spawnPos, Quaternion.identity);
                    return;
                }
                else
                { xDist -= 0.1f; }
            }
        }
        catch { Debug.Log("Didn't find place for the item spawn"); }
    }
}
