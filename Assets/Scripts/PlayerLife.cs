using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;
    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(deathSoundEffect == null)
        {
            Debug.Log("Please use a death sound effect");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            die();
        }
    }

    private void die()
    {
        animator.SetTrigger("Death");
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        GameObject.FindWithTag("DeathCount").GetComponent<DeathCount>().Death();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
