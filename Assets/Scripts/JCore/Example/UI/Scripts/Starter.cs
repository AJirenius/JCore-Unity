using System;
using JCore.Tracking;
using JCore.UI;
using UnityEditor.PackageManager;
using UnityEngine;
public class Starter : MonoBehaviour
{
    void Start()
    {
        ScreenManager.Instance.StartNewQueue("Screen1", new FirstScreenParams("A param string", 1098768));
        ErrorTracking.Instance.SetUser("1234", "Andy");
        ErrorTracking.Instance.breadcrumbs.AddBreadCrumb("Starting point");
    }
}
