using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Transform))]
public class LootSpawn : MonoBehaviour
{
    private float distBtwItems = 0.5f;
    private float yDist = - 0.5f;
    private float xDist = 1f;   // distance to loot spawn position
    private float rnd;

    public Loot[] items;


    public void ChangeLootSpawnPosition(float xDist, float yDist, float distBtwItems)
    {
        this.distBtwItems = distBtwItems;
        this.xDist = xDist;
        this.yDist = yDist;
    }

    public void SpawnLoot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            rnd = Random.Range(0, 100);

            if (rnd <= items[i].dropChance)
            {
                Vector2 spawnPos = new Vector2(transform.position.x + xDist + distBtwItems * i, transform.position.y + yDist);
                Instantiate(items[i].item, spawnPos, Quaternion.identity);
            }
        }
    }
}
