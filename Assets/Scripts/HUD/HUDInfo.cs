using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInfo : MonoBehaviour
{
    public string position;
    public int laps;
    void FixedUpdate()
    {
            this.gameObject.GetComponent<Text>().text = "Lap " + (laps + 1).ToString() + " of " + GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().lapsToComplete.ToString() + " | " + position;
    }
}
