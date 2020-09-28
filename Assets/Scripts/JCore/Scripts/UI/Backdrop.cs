using System.Collections.Generic;
using UnityEngine;
namespace JCore.UI
{
    /// <summary>
    /// Basic backdrop to shade underneath visuals.
    /// Add script to an UI object and set the whole gameobject as backdrop in Inspector of ScreenManager
    /// To enable backdrop, check the useBackdrop flag in the inspector of a AScreen object (popup or screen)
    /// </summary>
    public class Backdrop : MonoBehaviour
    {
        
        public void Initialize()
        {
            this.gameObject.SetActive(false);
        }
        
        public void Open(AScreen parent)
        {
            if (parent.useBackdrop)
            {
                this.gameObject.SetActive(true);
                int index = parent.transform.GetSiblingIndex();
                this.transform.SetSiblingIndex(index - 1);
            }
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}