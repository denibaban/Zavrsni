using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkJoin : MonoBehaviour
{
    public GameObject descriptionText;
    public NetworkManager netManager;
    public string hostIP;
    public void ConnectClient()
    {
        CleanIP();
        netManager = GameObject.FindGameObjectWithTag("Net Menager").GetComponent<NetworkManager>();
        netManager.networkAddress = hostIP;
        netManager.StartClient();
    }

    public void SetDescription()
    {
        CleanIP();
        descriptionText.GetComponent<Text>().text = hostIP;
    }

    void CleanIP()
    {
        for (int i = hostIP.Length - 1; i > 0; i--)
        {
            if (hostIP[i] == ':')
            {
                hostIP = hostIP.Remove(0, i+1);
                break;
            }
        }
    }
}
