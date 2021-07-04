using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatastreamRandomMovement : MonoBehaviour
{
    public bool xAxis;
    public bool yAxis;
    public bool zAxis;
    readonly float movementSpeed = 5;
    float movementTime;
    float elapsedTime;
    bool movementBack;
    void Start()
    {
        movementTime = Random.Range(10f, 20f);
        elapsedTime = movementTime;
        movementBack = false;
    }

    void Update()
    {
        elapsedTime -= Time.deltaTime;
        if (elapsedTime < 0)
        {
            movementBack = !movementBack;
            elapsedTime = movementTime;
        }
        if (xAxis)
        {
            if (movementBack)
            {
                gameObject.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
            else
            {
                gameObject.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
        }
        if (yAxis)
        {
            if (movementBack)
            {
                gameObject.transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
            }
            else
            {
                gameObject.transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
            }
        }
        if (zAxis)
        {
            if (movementBack)
            {
                gameObject.transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            }
            else
            {
                gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
        }
    }
}
