using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public int damage { get; private set; }


    public ActiveWeapon(int damage)
    { this.damage = damage; }


}
