using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkFind : NetworkDiscovery
{
    List<string> recievedBroadcasts = new List<string>();
    public void StartBroadcasts()
    {
        this.GetComponent<NetworkDiscovery>().Initialize();
        this.GetComponent<NetworkDiscovery>().StartAsClient();
        InvokeRepeating("BroadcastCleanup", 0f, 6f);
    }
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        UpdateBroadcastList(fromAddress);
    }

    void UpdateBroadcastList(string ip)
    {
        bool found = false;
        foreach (string item in recievedBroadcasts)
        {
            if (item == ip) found = true;
        }
        if (!found) recievedBroadcasts.Add(ip);
    }

    void BroadcastCleanup()
    {
        recievedBroadcasts.Clear();
    }

    public List<string> GetBroadcasts()
    {
        return recievedBroadcasts;
    }

    private void Start()
    {
        StartBroadcasts();
    }
}
