using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RatioCamera : MonoBehaviour
{
    [SerializeField]
    public float targetSize = 1.0f;

    [SerializeField]
    public RatioFit fit = RatioFit.LockHorizontal;

    [SerializeField]
    public float targetRatio = 1.7777777778f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        SetSize();

#if UNITY_EDITOR
#elif UNITY_IOS || UNITY_ANDROID
    this.enabled = false;
#endif
    }

    void Update()
    {
        SetSize();
    }
    
    private void SetSize()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float size;
        if ((fit == RatioFit.Fit && screenRatio > targetRatio) || fit == RatioFit.LockVertical)
        {
            size = targetSize;
        }
        else
        {
            size = targetSize * (targetRatio / screenRatio);
        }
        cam.orthographicSize = size;
    }
}

public enum RatioFit { LockHorizontal, LockVertical, Fit };