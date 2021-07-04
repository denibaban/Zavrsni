using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFadeout : MonoBehaviour
{
    public float duration = 0.0f;
    private float remainingTime;
    void Start()
    {
        remainingTime = duration;
    }
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            this.gameObject.GetComponent<CanvasRenderer>().SetAlpha(remainingTime / duration);
        }
        else Destroy(this.gameObject);
    }
}
