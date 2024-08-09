using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class PlayerData
{
    public float speed;
    public int defence;
    public int health;
    public int power;
    public int level;
    public float[] position;

    public PlayerData(PlayerController player) 
    {
        level = player.level;
        power = player.power;
        health = player.health;
        defence = player.defence;
        speed = player.speed;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
