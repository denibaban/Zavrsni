using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerupShield : NetworkBehaviour
{
    public GameObject user;
    public float duration = 10;
    float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        gameObject.transform.position = user.transform.position;
        if(startTime + duration < Time.time)
        {
            Blow();
        }
    }

    public void Blow()
    {
        Destroy(this.gameObject);
    }
}
