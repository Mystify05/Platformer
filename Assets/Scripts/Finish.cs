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
            Debug.LogError(this.gameObject + " Death Count is null");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            deathCount.GetComponent<DeathCount>().Reset();
            PlayerPrefs.SetInt("CheckPoint", 0);
            PlayerPrefs.Save();
        }
    }
}
