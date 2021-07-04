using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingTargetMove : MonoBehaviour
{
    public bool isKart;
    bool movingRight = true;
    void Update()
    {
        if (isKart) MoveKart();
        else MoveRocketTarget();
    }

    void MoveRocketTarget()
    {
        if (movingRight)
        {
            if (gameObject.transform.position.z > -10) gameObject.transform.Translate(Vector3.forward * -3 * Time.deltaTime);
            else movingRight = false;
        }
        else
        {
            if (gameObject.transform.position.z < 10) gameObject.transform.Translate(Vector3.forward * 3 * Time.deltaTime);
            else movingRight = true;
        }
    }

    void MoveKart()
    {
        if (movingRight)
        {
            if (gameObject.transform.position.x > -10) gameObject.transform.Translate(Vector3.forward * 3 * Time.deltaTime);
            else movingRight = false;
        }
        else
        {
            if (gameObject.transform.position.x < 10) gameObject.transform.Translate(Vector3.forward * -3 * Time.deltaTime);
            else movingRight = true;
        }
    }
}
