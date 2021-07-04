using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRocketShooter : MonoBehaviour
{
    public GameObject rocket;
    Vector3 startPosition;
    private void Start()
    {
        startPosition = this.transform.position + new Vector3(0, 0, 1);
        InvokeRepeating("LaunchRocket", 5f, 5f);
    }

    void LaunchRocket()
    {
        Instantiate(rocket, startPosition, Quaternion.identity);
    }
}
