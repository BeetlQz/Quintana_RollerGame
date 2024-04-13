using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRB;
    private GameObject player;

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        //change if multiplayer
        //Find("Player1") && Find("Player2")
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
            //- transform.position means the enemy's position
        }
    }
}

