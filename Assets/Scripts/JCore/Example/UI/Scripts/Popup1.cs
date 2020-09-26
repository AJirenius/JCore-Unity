using System.Collections;
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

        private void OnDialogueResult(DialogueResult result)
        {
            Debug.Log("RESULT:"+result.cont);
            if (result.cont == true)
            {
                
            }
            else
            {
                
            }
        }

        public void OnButton2Press()
        {
            Gem.Register<DialogueResult>(OnDialogueResult);
            ScreenManager.Instance.AddToQueue("Popup1");
        }
    }
}