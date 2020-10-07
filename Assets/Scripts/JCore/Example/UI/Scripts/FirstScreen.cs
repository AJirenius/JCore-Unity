using System.Collections;
using System.Collections.Generic;
using JCore.Tracking;
using UnityEngine;

namespace JCore.UI
{
    public class FirstScreen : Screen<FirstScreenParams>
    {
        public string button1ScreenId;
        public AScreen button2Screen;
        private GameObject nullObject;
        
        public void OnButton1Press()
        {
            ScreenManager.Instance.AddToQueue(button1ScreenId);
        }

        public void OnButton2Press()
        {
            ScreenManager.Instance.AddToQueue(button2Screen);
        }

        public void OnErrorButtonPress()
        {
            Debug.LogError("This is a Debug.LogError");
            nullObject.name = "will cause exception error";
        }

        override protected void OnParamsSet()
        {
            Debug.Log("Params set:" + param.name + " number:" + param.number);
        }

        override protected void OnAnimInStart()
        {
            Debug.Log("Anim In Start:" + param.name + " number:" + param.number);
        }

        override protected void OnAnimInEnd()
        {
            Debug.Log("Anim In End:" + param.name + " number:" + param.number);
        }
    }
}