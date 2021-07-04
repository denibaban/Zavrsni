using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownDummy : MonoBehaviour
{
    public bool gameStarted;
    float counter;
    void Start()
    {
        gameStarted = false;
        counter = 3;
    }

    void Update()
    {
        if(counter > 0)
            counter -= Time.deltaTime;
        else
        {
            gameStarted = true;
        }
    }

    public int GetCountdownTime()
    {
        return Mathf.RoundToInt(counter);
    }
}
