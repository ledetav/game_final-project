using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsateAnimation : MonoBehaviour
{
    public float minScale = 0.8f;
    public float maxScale = 1.2f;
    public float pulseSpeed = 1.0f;

    private float targetScale = 1.0f;
    private float currentScale = 1.0f;

    void Update()
    {
        currentScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * pulseSpeed);
        transform.localScale = Vector3.one * currentScale;
        
        if (Mathf.Abs(currentScale - targetScale) < 0.01f)
        {
            if (targetScale == maxScale)
                targetScale = minScale;
            else
                targetScale = maxScale;
        }
    }
}