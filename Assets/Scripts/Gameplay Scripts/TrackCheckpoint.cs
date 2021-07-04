using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoint : MonoBehaviour
{
    public int checkpointId;
    public GameObject gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //gm.GetComponent<GameManager>().CheckpointEnter(collision.gameObject, checkpointId);
            collision.gameObject.GetComponent<PlayerData>().CheckpointEnter(checkpointId);
        }
    }
}
