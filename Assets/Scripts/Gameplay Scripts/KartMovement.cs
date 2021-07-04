using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine;

public class KartMovement : NetworkBehaviour
{
    public float boost = 100;
    public float acceleration = 1000;
    public float steeringForce = 1500;
    public float jumpForce = 10000;
    public float boostForce = 10000;
    public float rotationSpeed = 4;
    public float desiredHeight = 1;
    public float springStiffness = 7500;
    public float springDamp = 1500;
    public bool allowMovement;
    bool airJump;
    public float wheelForce = 4200;
    float[] previousHeight;
    Rigidbody rb;
    Vector3[] wheels;
    RaycastHit[] wheelsRay;
    void Start()
    {
        wheels = new Vector3[4];
        previousHeight = new float[4];
        if (isLocalPlayer)
        {
            allowMovement = false;
            rb = gameObject.GetComponent<Rigidbody>();
            InvokeRepeating("BoostRegen", 1f, 1f);
            InvokeRepeating("AirJumpReset", 1f, 5f);
        }
        if (SceneManager.GetActiveScene().name == "Training") allowMovement = true;
    }

    void Update()
    {
        
        //Wheels
        wheels[0] = gameObject.transform.position + gameObject.transform.forward + gameObject.transform.right / 2 - gameObject.transform.up * 0.4f;
        wheels[1] = gameObject.transform.position + gameObject.transform.forward - gameObject.transform.right / 2 - gameObject.transform.up * 0.4f;
        wheels[2] = gameObject.transform.position - gameObject.transform.forward + gameObject.transform.right / 2 - gameObject.transform.up * 0.4f;
        wheels[3] = gameObject.transform.position - gameObject.transform.forward - gameObject.transform.right / 2 - gameObject.transform.up * 0.4f;

        int groundedWheels = 0;
        for (int i = 0; i < 4; i++)
        {
            RaycastHit temp;
            Physics.Raycast(wheels[i], -transform.up, out temp, desiredHeight, 9);
            float actualHeight = temp.distance;
            float springRatio = (desiredHeight - actualHeight) / desiredHeight;
            float springVelocity = (previousHeight[i] - actualHeight) / Time.deltaTime;
            Vector3 springForce = transform.up * (springStiffness * springRatio + springDamp * springVelocity);
            if (temp.collider != null)
            {
                rb.AddForceAtPosition(springForce, wheels[i]);
                groundedWheels++;
            }
                
            previousHeight[i] = actualHeight;
        }
        

        if (isLocalPlayer && allowMovement)
        {
            if (groundedWheels > 0)
            {
                
                //Driving
                if (Input.GetAxis("Vertical") != 0)
                    rb.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * acceleration);
                if (Input.GetAxis("Horizontal") != 0)
                    rb.AddRelativeTorque(Vector3.up * Input.GetAxis("Horizontal") * (Input.GetAxis("Vertical") >= 0 ? 1 : -1) * steeringForce);
                //Jump
                airJump = true;
                if (Input.GetKeyDown(KeyCode.Space) && groundedWheels > 2)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
            else
            {
                //Rotation
                gameObject.transform.Rotate(new Vector3(1, 0, 0) * Input.GetAxis("Vertical") * rotationSpeed, Space.Self);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    gameObject.transform.Rotate(new Vector3(0, 0, -1) * Input.GetAxis("Horizontal") * rotationSpeed, Space.Self);
                }
                else
                {
                    gameObject.transform.Rotate(new Vector3(0, 1, 0) * Input.GetAxis("Horizontal") * rotationSpeed, Space.Self);
                }
                //Flip
                if (Input.GetKeyDown(KeyCode.Space) && airJump)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    airJump = false;
                }
            }

            //Boost
            if (Input.GetKey(KeyCode.Mouse1) && boost > 0)
            {
                boost -= Time.deltaTime * 30;
                GameObject.FindGameObjectWithTag("HUD Boost").GetComponent<HUDBoost>().SetBoost(boost);
                rb.AddRelativeForce(Vector3.forward * boostForce);
                gameObject.GetComponent<TrailRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<TrailRenderer>().enabled = false;
            }

            //Hooked movement
            Collider[] hookCollider = Physics.OverlapSphere(this.gameObject.transform.position, 2);
            foreach (Collider item in hookCollider)
            {
                if (item.GetComponent<PowerupHook>())
                    if(item.GetComponent<PowerupHook>().User != this.gameObject)
                        gameObject.transform.position = item.transform.position;
            }
        }
    }

    void BoostRegen()
    {
        if (isLocalPlayer && allowMovement)
        {
            if (!Input.GetKey(KeyCode.Mouse1))
            {
                if (boost < 100) boost += 2;
                if (boost > 100) boost = 100;
                GameObject.FindGameObjectWithTag("HUD Boost").GetComponent<HUDBoost>().SetBoost(boost);
            }
        }
    }

    void AirJumpReset()
    {
        airJump = true;
    }
}