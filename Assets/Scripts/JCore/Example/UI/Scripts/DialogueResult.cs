using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore
{
    public class DialogueResult : AGameEvent
    {
        public bool cont = false;
    
        public DialogueResult(bool cont)
        {
            this.cont = cont;
        }   
    }
}