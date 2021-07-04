using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDisplay : MonoBehaviour
{
    public GameObject listingPrefab;
    public GameObject netDiscovery;
    private void Start()
    {
        InvokeRepeating("CreateListings", 1f, 5f);
    }

    void CreateListings()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        int listingCounter = 0;
        foreach (string item in netDiscovery.GetComponent<NetworkFind>().GetBroadcasts())
        {
            GameObject newListing = Instantiate(listingPrefab, this.transform);
            newListing.transform.localPosition = new Vector2(0, -100 * listingCounter);
            newListing.GetComponent<NetworkJoin>().hostIP = item;
            newListing.GetComponent<NetworkJoin>().SetDescription();
        }
    }
}
