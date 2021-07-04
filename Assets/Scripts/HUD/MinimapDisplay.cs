using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDisplay : MonoBehaviour
{
    public GameObject minimapPlayerPrefab;

    List<MinimapPlayerIcon> playerIcons;
    public float minimapScale = 1; // mapsize / 125
    private void Start()
    {
        playerIcons = new List<MinimapPlayerIcon>();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            GameObject minimapIcon = Instantiate(minimapPlayerPrefab, this.gameObject.transform);
            minimapIcon.transform.localPosition = new Vector3(125, -125, 0);
            MinimapPlayerIcon newIcon = new MinimapPlayerIcon(player, minimapIcon);
            playerIcons.Add(newIcon);
        }
    }
    void FixedUpdate()
    {
        foreach (MinimapPlayerIcon icon in playerIcons)
        {
            icon.DotDisplay.transform.localPosition = new Vector3(icon.Owner.transform.position.x, icon.Owner.transform.position.z, 0) / minimapScale;
        }
    }
}

public class MinimapPlayerIcon
{
    public GameObject Owner { get; }
    public GameObject DotDisplay { get; }
    public MinimapPlayerIcon(GameObject owner, GameObject dotDisplay)
    {
        Owner = owner;
        DotDisplay = dotDisplay;
    }
}
