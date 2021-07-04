using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerupHook : NetworkBehaviour
{
    public GameObject User;
    public GameObject Hooked;
    public GameObject RopePrefab;
    public bool alternativeMode;
    public float maxDistance = 30;
    public float speed = 30;

    bool hookHit;
    bool retracting;
    GameObject Rope;

    void Start()
    {
        hookHit = false;
        retracting = false;
        Rope = Instantiate(RopePrefab, gameObject.transform.position, Quaternion.identity);
    }

    void Update()
    {
        Vector3 hookLine = User.transform.position - gameObject.transform.position;
        if (hookLine.magnitude > maxDistance) retracting = true;
        else if (hookLine.magnitude < 2 && retracting)
        {
            Destroy(this.gameObject);
            Destroy(Rope);
        }
        if (alternativeMode)
        {
            if (retracting)
            {
                if (hookHit)
                {
                    gameObject.transform.position = Hooked.transform.position;
                    User.transform.position = Vector3.Lerp(User.transform.position, gameObject.transform.position, 0.02f);
                }
                else
                {
                    gameObject.transform.position = Vector3.Lerp(User.transform.position, gameObject.transform.position, 0.98f);
                }
            }
            else
            {
                gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            }
        }
        else
        {
            if (retracting)
            {
                gameObject.transform.position = Vector3.Lerp(User.transform.position, gameObject.transform.position, 0.98f);
                if (hookHit)
                {
                    Hooked.transform.position = gameObject.transform.position;
                }
            }
            else
            {
                gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            }
        }

        //Rope
        Rope.transform.position = User.transform.position - hookLine / 2;
        Rope.transform.localScale = new Vector3(0.05f, hookLine.magnitude - 0.5f * hookLine.magnitude, 0.05f);
        Rope.transform.rotation = Quaternion.LookRotation(hookLine);
        Rope.transform.Rotate(90, 0, 0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player" || collider.gameObject.tag == "Mine" && !hookHit)
        {
            if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<PowerupController>().shield)
                collider.gameObject.GetComponent<PowerupController>().BlowShield();
            else
            {
                Hooked = collider.gameObject;
                retracting = true;
                hookHit = true;
            }
        }
    }
}
