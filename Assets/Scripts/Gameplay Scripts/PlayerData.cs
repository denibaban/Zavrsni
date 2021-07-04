using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{
    public int laps;
    public int[] checkpoints;
    public float positionValue;
    public int lastCheckpointId;
    private void Start()
    {
        laps = 0;
        lastCheckpointId = -1;
        checkpoints = new int[GameObject.FindGameObjectsWithTag("Checkpoint").Length];
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Checkpoint").Length; i++) checkpoints[i] = 0;
    }

    private void Update()
    {
        SetPositionValue();
        if (SceneManager.GetActiveScene().name != "Training" && isLocalPlayer)
        {
            GameObject.FindGameObjectWithTag("HUD Info").GetComponent<HUDInfo>().position = CalculatePosition();
            GameObject.FindGameObjectWithTag("HUD Info").GetComponent<HUDInfo>().laps = laps; 
        }   
    }

    void SetPositionValue()
    {
        int nextCheckpointIndex = 0;
        positionValue = laps * 1000 * GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i] == 1) positionValue += 1000;
            else
            {
                nextCheckpointIndex = i;
                break;
            }
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            if (item.GetComponent<TrackCheckpoint>().checkpointId == nextCheckpointIndex)
            {
                Vector3 nextCheckpointDistance = this.gameObject.transform.position - item.transform.position;
                positionValue -= nextCheckpointDistance.magnitude;
            }
        }
    }

    public void CheckpointEnter(int id)
    {
        if(checkpoints[id] == 0)
        {
            if(id == 0)
            {
                checkpoints[id] = 1;
                lastCheckpointId = id;
            }
            else if(checkpoints[id-1] == 1)
            {
                checkpoints[id] = 1;
                lastCheckpointId = id;
            }
        }
    }

    public void StartlineEnter()
    {
        bool allCheckpointsPassed = true;
        for (int i = 0; i < checkpoints.Length; i++)
            if (checkpoints[i] == 0) allCheckpointsPassed = false;
        if(allCheckpointsPassed)
        {
            laps += 1;
            lastCheckpointId = -1;
            for (int i = 0; i < checkpoints.Length; i++) checkpoints[i] = 0;
        }
        if(laps == GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().lapsToComplete)
        {
            GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().FinishGame();
            gameObject.GetComponent<KartMovement>().allowMovement = false;
        }
    }

    public GameObject GetLastPassedCheckpoint()
    {
        if(lastCheckpointId != -1)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Checkpoint"))
            {
                if (item.GetComponent<TrackCheckpoint>().checkpointId == lastCheckpointId) return item;
            }
        }
        return GameObject.FindGameObjectWithTag("Start");
    }

    public string CalculatePosition()
    {
        string position;
        int positionCounter = 1;
        foreach (GameObject kart in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (kart != this.gameObject && kart.GetComponent<PlayerData>().positionValue > this.gameObject.GetComponent<PlayerData>().positionValue) positionCounter += 1;
        }
        if (positionCounter == 1) position = "1st";
        else if (positionCounter == 2) position = "2nd";
        else if (positionCounter == 3) position = "3rd";
        else position = positionCounter.ToString() + "th";
        return position;
    }
}
