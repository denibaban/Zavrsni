using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHosting : MonoBehaviour
{
    NetworkManager networkManager;
    public GameObject mapPicker;

    private void Start()
    {
        networkManager = gameObject.GetComponent<NetworkManager>();
        networkManager.networkAddress = GetLocalIP();
    }

    public void StartHosting()
    {
        networkManager.onlineScene = mapPicker.GetComponent<MapPicker>().GetSelectedMap();
        networkManager.StartHost();
    }

    string GetLocalIP()
    {
        IPHostEntry host;
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress item in host.AddressList)
        {
            if (item.AddressFamily == AddressFamily.InterNetwork)
            {
                return item.ToString();
            }
        }
        return "";
    }
}
