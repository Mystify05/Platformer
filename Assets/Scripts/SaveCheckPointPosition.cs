using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCheckPointPosition : MonoBehaviour
{
    public static List<Vector3> CheckPointPosition { set; get; }
    [SerializeField] private Transform player;
    private void Start()
    {
        CheckPointPosition.Add(player.position);
        player.GetComponent<PlayerMovement>().MoveToCheckPoint();
    }
}