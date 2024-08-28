using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


[RequireComponent(typeof(Transform))]
public class LootSpawn : MonoBehaviour
{
    private float distBtwItems = 0.5f;
    private float yDist = - 0.5f;
    private float xDist = 1f;   // distance to loot spawn position
    float rayLength = 0.1f;
    private float rnd;

    public LayerMask whatIsObstacle;
    public Loot[] items;


    public void ChangeLootSpawnPosition(float xDist, float yDist, float distBtwItems)
    {
        this.distBtwItems = distBtwItems;
        this.xDist = xDist;
        this.yDist = yDist;
    }

    public void SpawnLoot()
    {
        try
        {
            for (int i = 0; i < items.Length; i++)
            {
                rnd = Random.Range(0, 100);

                while (true)       // choose spawn place
                if (rnd <= items[i].dropChance)
                {
                    Vector2 spawnPos = new Vector2(transform.position.x + xDist + distBtwItems * i, transform.position.y + yDist);
                    RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, rayLength, whatIsObstacle);

                    if (hit.collider == null)
                    {
                        Instantiate(items[i].item, spawnPos, Quaternion.identity);
                        break;
                    }
                    else
                    { xDist -= 0.1f; }
                }
            }
        }
        catch
        { Debug.Log("Didn't find place for the item spawn"); }
    }
}
