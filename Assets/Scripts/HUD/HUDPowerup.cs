using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPowerup : MonoBehaviour
{
    public void SetImage(int powerupId)
    {
        switch (powerupId)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupRocket");
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupHook");
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupShield");
                break;
            case 4:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupMine");
                break;
        }
    }

    public void RemoveImage()
    {
        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupNone");
    }
}
