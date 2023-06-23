using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currenWaypointIndex = 0;
    private bool up = true;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currenWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            if (currenWaypointIndex >= waypoints.Length -1)
            {
                up = false;
            }
            else if(currenWaypointIndex <= 0)
            {
                up = true;
            }
            if (up)
            {
                currenWaypointIndex++;
            }
            else
            {
                currenWaypointIndex--;
            }
        }
        //Vector2 direction = waypoints[currenWaypointIndex].transform.position - transform.position;
        //transform.Translate(direction.normalized * Time.deltaTime * speed);
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currenWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
