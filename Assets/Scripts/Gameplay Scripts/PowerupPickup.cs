using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        int powerupId = Random.Range(1, 5);
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<PowerupController>().GainPowerup(powerupId);
        }
    }
}
