using System.Collections.Generic;
using UnityEngine;
namespace JCore.UI
{
    public class Screen<T> : AScreen where T : AViewParams
    {
        public T param;
        public override void SetParams(AViewParams param)
        {
            this.param = param as T;
        }
    }
}