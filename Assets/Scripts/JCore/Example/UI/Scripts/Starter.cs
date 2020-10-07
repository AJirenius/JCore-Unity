using System;
using JCore.Tracking;
using JCore.UI;
using UnityEditor.PackageManager;
using UnityEngine;
public class Starter : MonoBehaviour
{
    void Start()
    {
        ErrorTracking track = ErrorTracking.Instance;
        track.SetUser("1234", "Andy");
        track.tags.AddTag("DEBUG");
        track.tags.AddTag(Application.version);
        ErrorTracking.Instance.breadcrumbs.AddBreadCrumb("Starting point");
        
        ScreenManager.Instance.StartNewQueue("Screen1", new FirstScreenParams("A param string", 1098768));

    }
}
