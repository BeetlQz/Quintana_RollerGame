using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public List<Transform> waypoints;

    private int waypointIndex;
    private float range;


    private void Start()
    {
        waypointIndex = 0;
        range = 1f;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.LookAt(waypoints[waypointIndex]);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, waypoints[waypointIndex].position) > range )
        {
            waypointIndex++;
            if(waypointIndex >= waypoints.Count )
            {
                waypointIndex = 0;
            }
        }
    }

}
