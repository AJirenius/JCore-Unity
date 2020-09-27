using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore.UI
{
    public class Popup1 : Screen<Popup1Params>
    {
        public void OnButton1Press()
        {
            Debug.Log("BACK");
            ScreenManager.Instance.Back();
            param.onOk.Invoke();
        }
        
        public void OnButton2Press()
        {
            Debug.Log("BACK");
            ScreenManager.Instance.Back();
            param.onCancel.Invoke();
        }
    }
}