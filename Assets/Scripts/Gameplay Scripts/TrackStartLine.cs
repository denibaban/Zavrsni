using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackStartLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerData>().StartlineEnter();
        }
    }
}
