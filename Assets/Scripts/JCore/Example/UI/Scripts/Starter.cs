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
        track.breadcrumbs.AddBreadCrumb("Starting point");
        track.userCustomData.Add("Ingame rounds","4");
        track.userCustomData.Add("VIP","true");
        track.userCustomData.Add("VIP","false");
        track.userCustomData.Add("main Unit","Behemoth");

        ScreenManager.Instance.StartNewQueue("Screen1", new FirstScreenParams("A param string", 1098768));

    }
}
