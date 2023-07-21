using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] private int CheckPointId;
    [SerializeField] private Transform player;
    private Animator animator;
    private static int checkPoint = 0;
    public int CheckPoint { get { return checkPoint; } }
    private void Start()
    {
        checkPoint = PlayerPrefs.GetInt("CheckPoint", 0);
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform.parent;

        //if (CheckPointId == 0) Debug.LogError(this.gameObject + " Please give this Object an Id");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (checkPoint >= CheckPointId)
            return;*/
        animator.SetTrigger("Check");
        checkPoint = CheckPointId;
        PlayerPrefs.SetInt("CheckPoint", checkPoint);
        PlayerPrefs.Save();
    }
}
