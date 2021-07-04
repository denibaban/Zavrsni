using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerupController : NetworkBehaviour
{
    public GameObject rocketPrefab;
    public GameObject hookPrefab;
    public GameObject shieldPrefab;
    public GameObject minePrefab;
    public float mineThrowForce = 200;
    public float shieldExplosionForce = 15000;
    GameObject HUD;
    GameManager gm;

    public int powerupId = 0;
    public GameObject shield;

    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        HUD = GameObject.FindGameObjectWithTag("HUD Powerup");
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && powerupId != 0 && gm.isGameActive && isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.LeftShift)) UsePowerup(true);
            else UsePowerup(false);
        }
    }

    public void GainPowerup(int id)
    {
        if (powerupId == 0) powerupId = id;
        if (isLocalPlayer)
        {
            HUD.GetComponent<HUDPowerup>().SetImage(powerupId);
        }
    }

    public void BlowShield()
    {
        shield.GetComponent<PowerupShield>().Blow();
    }

    void UsePowerup(bool altMode)
    {
        switch (powerupId)
        {
            case 1:
                UseRocket(altMode);
                break;
            case 2:
                UseHook(altMode);
                break;
            case 3:
                UseShield(altMode);
                break;
            case 4:
                UseMine(altMode);
                break;
        }
        powerupId = 0;
        HUD.GetComponent<HUDPowerup>().RemoveImage();
    }

    void UseRocket(bool altMode)
    {
        if (altMode)
        {
            GameObject rocket = Instantiate(rocketPrefab, gameObject.transform.position - 2.5f * gameObject.transform.forward, Quaternion.Euler(new Vector3(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y - 180, gameObject.transform.rotation.eulerAngles.z)));
            NetworkServer.Spawn(rocket);
        }
        else
        {
            GameObject rocket = Instantiate(rocketPrefab, gameObject.transform.position + 2.5f * gameObject.transform.forward, gameObject.transform.rotation);
            NetworkServer.Spawn(rocket);
        }
    }

    void UseHook(bool altMode)
    {
        GameObject hook = Instantiate(hookPrefab, gameObject.transform.position + 3f * gameObject.transform.forward, gameObject.transform.rotation);
        hook.GetComponent<PowerupHook>().User = this.gameObject;
        NetworkServer.Spawn(hook);
        if (altMode) hook.GetComponent<PowerupHook>().alternativeMode = true;
        else hook.GetComponent<PowerupHook>().alternativeMode = false;
    }

    void UseShield(bool altMode)
    {
        if (altMode)
        {
            Collider[] explosionCollider = Physics.OverlapSphere(this.gameObject.transform.position, 5);
            foreach (Collider item in explosionCollider)
            {
                Vector3 explosionDirection = item.transform.position - this.gameObject.transform.position;
                if (item.GetComponent<Rigidbody>())
                {
                    if (item.GetComponent<PowerupController>())
                    {
                        if (item.GetComponent<PowerupController>().shield)
                            item.GetComponent<PowerupController>().BlowShield();
                        else
                        {
                            item.GetComponent<Rigidbody>().AddForce(explosionDirection * shieldExplosionForce, ForceMode.Impulse);
                        }
                    }
                    else
                    {
                        item.GetComponent<Rigidbody>().AddForce(explosionDirection * shieldExplosionForce/100, ForceMode.Impulse);
                    }
                }
            }
        }
        else
        {
            shield = Instantiate(shieldPrefab, gameObject.transform.position, Quaternion.identity);
            NetworkServer.Spawn(shield);
            shield.GetComponent<PowerupShield>().user = this.gameObject;
        }
    }

    void UseMine(bool altMode)
    {
        if (altMode)
        {
            GameObject mine = Instantiate(minePrefab, gameObject.transform.position + 3 * gameObject.transform.forward, gameObject.transform.rotation);
            NetworkServer.Spawn(mine);
            mine.GetComponent<Rigidbody>().AddRelativeForce(mineThrowForce * (Vector3.forward + Vector3.up/2), ForceMode.Impulse);
        }
        else
        {
            GameObject mine = Instantiate(minePrefab, gameObject.transform.position - 3 * gameObject.transform.forward, Quaternion.identity);
            NetworkServer.Spawn(mine);
        }
    }
}
