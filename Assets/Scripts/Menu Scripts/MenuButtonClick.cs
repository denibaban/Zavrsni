using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine;

public class MenuButtonClick : MonoBehaviour
{
    public GameObject mapPicker;
    public void OpenSubmenu()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseSubmenu()
    {
        this.gameObject.SetActive(false);
    }

    public void OptionExit()
    {
        Application.Quit();
    }

    public void OptionTraining()
    {
        NetworkManager nm = GameObject.FindGameObjectWithTag("Net Menager").GetComponent<NetworkManager>();
        nm.onlineScene = "Training";
        nm.StartHost();
    }

    public void StartHosting()
    {
        if(mapPicker != null)
        {
            NetworkManager nm = GameObject.FindGameObjectWithTag("Net Menager").GetComponent<NetworkManager>();
            nm.onlineScene = mapPicker.GetComponent<MapPicker>().GetSelectedMap();
            nm.StartHost();
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitToMainMenu()
    {
        NetworkManager nm = GameObject.FindGameObjectWithTag("Net Menager").GetComponent<NetworkManager>();
        nm.StopHost();
        //nm.client.Disconnect();
        
        //SceneManager.LoadScene("Menu");
    }
}
