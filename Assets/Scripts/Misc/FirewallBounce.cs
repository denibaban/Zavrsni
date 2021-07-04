using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallBounce : MonoBehaviour
{
    public float bounceForce = 1000;
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 bounceDirection = this.gameObject.transform.position - collision.gameObject.transform.position;
        collision.gameObject.GetComponent<Rigidbody>().AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
    }
}
