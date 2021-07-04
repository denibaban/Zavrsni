using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatastreamFloorReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.transform.position = collider.gameObject.GetComponent<PlayerData>().GetLastPassedCheckpoint().transform.position;
        }
    }
}
