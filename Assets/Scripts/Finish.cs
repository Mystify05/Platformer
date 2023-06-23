using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject deathCount;

    private void Start()
    {
        if(deathCount ==  null)
        {
            Debug.Log("Death Count is null");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            deathCount.GetComponent<DeathCount>().Reset();
        }
    }
}
