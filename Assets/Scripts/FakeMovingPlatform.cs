using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMovingPlatform : MonoBehaviour
{
    private WaypointFollower waypointFollower;
    private StickyPlatform stickyPlatform;
    //private Rigidbody2D rb;

    private bool falling = false;

    private float currendPosition;
    //private float startTime;

    void Start()
    {
        waypointFollower = GetComponent<WaypointFollower>();
        stickyPlatform = GetComponent<StickyPlatform>();
        //rb = GetComponent<Rigidbody2D>();

        if (waypointFollower == null)
            Debug.LogError(this.gameObject + " needs a waypointFollower Script");
        if (stickyPlatform == null)
            Debug.LogError(this.gameObject + " needs a StickyPlatform Script");

        waypointFollower.enabled = false;
        //rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if(falling)
        {
            //float speed = currendPosition - transform.position.y <= 0.01f ? 0.01f : currendPosition - transform.position.y / Mathf.Pow(Time.time - startTime, 2) / 1000;
            float speed = Mathf.Pow(1.25f, currendPosition - transform.position.y <= 0.001f ? 0.001f : currendPosition - transform.position.y) + 10;
            if (speed > 50)
                speed = 50;
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    private IEnumerator fall()
    {
        yield return new WaitForSeconds(2);
        currendPosition = transform.position.y;
        //startTime = Time.time;
        falling = true;
        waypointFollower.enabled = false;
        //rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        waypointFollower.enabled = true;
        StartCoroutine(fall());
    }
}