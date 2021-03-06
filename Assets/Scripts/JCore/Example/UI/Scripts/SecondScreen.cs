﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore.UI
{
    public class SecondScreen : Screen<NoParams>
    {
        public void OnButton1Press()
        {
            ScreenManager.Instance.Back();
        }

        public void OnButton2Press()
        {
            ScreenManager.Instance.AddToQueue("Popup1", new Popup1Params(
                () =>
                {
                    
                    Debug.Log("Open screen3");
                    ScreenManager.Instance.AddToQueue("Screen3");
                },
                () =>
                {
                    Debug.Log("Do nothing?");
                }));
        }
    }
}