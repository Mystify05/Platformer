using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockUp : MonoBehaviour
{
    [SerializeField] private float power = 8f;
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private bool collision = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.collision = true;
        collision.GetComponent<PlayerMovement>().Flying = true;
        rb = collision.GetComponent<Rigidbody2D>();
        startPosition = collision.transform.position;

        if(Vector2.Distance(startPosition, transform.position) < 2f)
        {
            rb.gravityScale = 0;
        }
        else
        {
            StartCoroutine(FanDelay());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(rb.gravityScale <= 0.1f && !collision.GetComponent<PlayerMovement>().IsDashing)
        {
            //rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * power);
            float calcPower = 0.1f * Mathf.Pow(Vector2.up.y * power, Vector2.Distance(collision.transform.position, transform.position)) + 3;
            if (calcPower > 12f)
                calcPower = 12f;
            rb.velocity = new Vector2(rb.velocity.x, calcPower);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.collision = false;
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        rb.gravityScale = playerMovement.OriginalGravity;
        playerMovement.Flying = false;
    }

    private IEnumerator FanDelay()
    {
        yield return new WaitForSeconds(0.2f);
        if(collision)
            rb.gravityScale = 0;
    }
}
