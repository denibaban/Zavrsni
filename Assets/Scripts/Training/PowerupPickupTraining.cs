using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickupTraining : MonoBehaviour
{
    public int powerup;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<PowerupController>().GainPowerup(powerup);
        }
    }
}
