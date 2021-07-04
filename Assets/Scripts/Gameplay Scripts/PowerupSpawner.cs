using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    GameObject sphere;
    float renewTime = 10;
    private void Start()
    {
        sphere = Instantiate(spherePrefab, this.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }
    void Update()
    {
        if(!sphere)
        {
            renewTime -= Time.deltaTime;
        }
        if(renewTime <= 0)
        {
            renewTime = 10;
            sphere = Instantiate(spherePrefab, this.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    }
}
