using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour
{
    public float cameraDistance = 5;
    public float cameraHeight = 2;
    public float camAngle = 10;

    void Update()
    {
        if (isLocalPlayer)
        {
            Vector3 cameraPosition;
            cameraPosition = gameObject.transform.position - (gameObject.transform.forward * cameraDistance);
            cameraPosition.y = gameObject.transform.position.y + cameraHeight;
            Camera.main.transform.position = cameraPosition;
            Camera.main.transform.eulerAngles = new Vector3(camAngle, gameObject.transform.eulerAngles.y, 0);
        }
    }
}
