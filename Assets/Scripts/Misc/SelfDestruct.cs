using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float timer = 5f; 
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else Destroy(this.gameObject);
    }
}
