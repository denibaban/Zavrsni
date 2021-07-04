using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBoost : MonoBehaviour
{
    public void SetBoost(float value)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(value * 3, 40);
    }
}
