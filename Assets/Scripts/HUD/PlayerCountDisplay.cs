using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerCountDisplay : MonoBehaviour
{
    void FixedUpdate()
    {
        gameObject.GetComponent<Text>().text = NetworkServer.connections.Count.ToString() + "/5";
    }
}
