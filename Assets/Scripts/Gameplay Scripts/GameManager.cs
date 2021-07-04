using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public GameObject exitWindow;
    public GameObject endWindow;
    public GameObject minimap;
    public GameObject netDiscovery;
    public GameObject countdownText;
    public GameObject countdownPrefab;

    public bool isGameActive;
    public int lapsToComplete = 3;

   
    float counter = 3;
    bool countdownActive = false;

    void Start()
    {
        if(SceneManager.GetActiveScene().name != "Training")
        {
            netDiscovery.GetComponent<NetworkDiscovery>().Initialize();
            netDiscovery.GetComponent<NetworkDiscovery>().StartAsServer();
            isGameActive = false;
        }
        else
        {
            isGameActive = true;
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            exitWindow.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (GameObject.FindGameObjectWithTag("Countdown")) countdownActive = true;
        if (countdownActive)
        {
            if (counter > 0)
            {
                countdownText.SetActive(true);
                counter -= Time.deltaTime;
                countdownText.GetComponent<Text>().text = Mathf.RoundToInt(counter).ToString();
            }
            else
            {
                foreach (GameObject kart in GameObject.FindGameObjectsWithTag("Player"))
                {
                    kart.GetComponent<KartMovement>().allowMovement = true;
                }
                countdownActive = false;
                Destroy(GameObject.FindGameObjectWithTag("Countdown"));
                countdownText.SetActive(false);
                minimap.SetActive(true);
                isGameActive = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void StartGame()
    {
        GameObject countdownObject = Instantiate(countdownPrefab);
        NetworkServer.Spawn(countdownObject);
        if (SceneManager.GetActiveScene().name != "Training") StopBroadcasting();
    }

    public void FinishGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endWindow.SetActive(true);
        GameObject.FindGameObjectWithTag("HUD Position").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("HUD Info").GetComponent<HUDInfo>().position;
    }

    public void StopBroadcasting()
    {
        netDiscovery.GetComponent<NetworkDiscovery>().StopBroadcast();
    }
}