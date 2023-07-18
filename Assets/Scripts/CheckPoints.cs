using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] private int CheckPointId;
    private Animator animator;
    private static GameObject player;
    private static int checkPoint = 0;
    public int CheckPoint { get { return checkPoint; } }
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        checkPoint = PlayerPrefs.GetInt("CheckPoint", 0);

        if (CheckPointId == 0)
            Debug.LogError(this.gameObject + " Please give this Object an Id");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkPoint >= CheckPointId)
            return;
        animator.SetTrigger("Check");
        checkPoint++;
        PlayerPrefs.SetInt("CheckPoint", checkPoint);
        PlayerPrefs.Save();
        SaveCheckPointPosition.CheckPointPosition.Add(player.transform.position);
    }
}
