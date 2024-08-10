using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private enum State
    { Roaming }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    [SerializeField] private int health = 100;
    //private GameObject bloodEffect;


    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    { StartCoroutine(RoamingRoutine()); }

    private void Update()
    { if (health <= 0) Destroy(gameObject); }

    public void GetHurt(int damage)
    {
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage; Debug.Log("Enemy is hurt");
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            float roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private float GetRoamingPosition()
    { return Random.Range(-1f, 1f); }
}
