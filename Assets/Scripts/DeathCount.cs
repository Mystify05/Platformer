using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCount : MonoBehaviour
{
    private static int deaths;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        deaths = PlayerPrefs.GetInt("Deaths", 0);
        text.SetText("Deaths: " + deaths);
    }

    public void Death()
    {
        deaths++;
        PlayerPrefs.SetInt("Deaths", deaths);
        PlayerPrefs.Save();
        text.SetText("Deaths: " + deaths);
    }

    public void Reset()
    {
        deaths = 0;
        PlayerPrefs.SetInt("Deaths", 0);
        text.SetText("Deaths: " + deaths);
    }
}
