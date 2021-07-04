using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerupMine : NetworkBehaviour
{
    public float explosionForce = 10000;
    public float explosionRadius = 5;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody>())
        {
            Collider[] explosionCollider = Physics.OverlapBox(gameObject.transform.position, transform.localScale * explosionRadius);
            foreach (Collider item in explosionCollider)
            {
                Vector3 explosionDirection = item.transform.position - this.gameObject.transform.position;
                if (item.GetComponent<Rigidbody>())
                {
                    if (item.GetComponent<PowerupController>())
                    {
                        if (item.GetComponent<PowerupController>().shield)
                            item.GetComponentInParent<PowerupController>().BlowShield();
                        else
                            item.GetComponent<Rigidbody>().AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
                    }
                    else
                        item.GetComponent<Rigidbody>().AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
