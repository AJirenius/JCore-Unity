using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore.UI
{
    public class Popup1 : Screen<Popup1Params>
    {
        public void OnButton1Press()
        {
            ScreenManager.Instance.Back();
            param.onOk.Invoke();
        }
        
        public void OnButton2Press()
        {
            ScreenManager.Instance.Back();
            param.onCancel.Invoke();
        }
    }
}