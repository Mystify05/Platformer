using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCheckPointPosition : MonoBehaviour
{
    //public static List<Vector3> CheckPointPosition = new List<Vector3>();
    [SerializeField] private GameObject[] checkPoints;
    public static Dictionary<int, Vector3> CheckPoints = new Dictionary<int, Vector3>();
    [SerializeField] private Transform player;
    private static bool start = false;
    private void Start()
    {
        for(int i = 0; i < checkPoints.Length; i++)
        {
            if (start)
                break;

            CheckPoints.Add(i, checkPoints[i].transform.position);
        }
        start = true;
        player.GetComponent<PlayerMovement>().MoveToCheckPoint(PlayerPrefs.GetInt("CheckPoint", 0));
    }
}