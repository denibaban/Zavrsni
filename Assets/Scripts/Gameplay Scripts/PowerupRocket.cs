using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerupRocket : NetworkBehaviour
{
    Rigidbody rb;
    public float thrust = 10;
    public float explosionForce = 15000;
    public float explosionRadius = 5;
    public GameObject explosionPrefab;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 500, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * thrust);
    }

    private void OnCollisionEnter()
    {
        Collider[] explosionCollider = Physics.OverlapSphere(this.gameObject.transform.position, 1);
        Destroy(this.gameObject);
        foreach (Collider item in explosionCollider)
        {
            Vector3 explosionDirection = item.transform.position - this.gameObject.transform.position;
            if (item.GetComponent<Rigidbody>())
            {
                if(item.GetComponent<PowerupController>())
                {
                    if(item.GetComponent<PowerupController>().shield)
                        item.GetComponent<PowerupController>().BlowShield();
                    else
                    {
                        item.GetComponent<Rigidbody>().AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
                    }
                }
                else
                {
                    item.GetComponent<Rigidbody>().AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
                }
            }
        }
    }
}
