using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MapPicker : MonoBehaviour
{
    readonly List<string> maps = new List<string>{"Datastream", "VioletValley"};
    public GameObject mapImage; 
    int currentSelection;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Net Menager").GetComponent<NetworkHosting>().mapPicker = this.gameObject;
        currentSelection = 0;
        SetMapImage();
    }
    public void SelectNextMap()
    {
        if (currentSelection == maps.Count - 1) currentSelection = 0;
        else currentSelection += 1;
        SetMapImage();
    }
    public void SelectPreviousMap()
    {
        if (currentSelection == 0) currentSelection = maps.Count - 1;
        else currentSelection -= 1;
        SetMapImage();
    }

    public string GetSelectedMap()
    {
        return maps[currentSelection];
    }

    void SetMapImage()
    {
        mapImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(maps[currentSelection]);
    }
}
