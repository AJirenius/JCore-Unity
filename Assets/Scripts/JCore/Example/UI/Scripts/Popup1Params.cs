using JCore.UI;
using UnityEngine.Events;

public class Popup1Params : AViewParams
{
    public UnityAction onOk;
    public UnityAction onCancel;
    
    public Popup1Params(UnityAction onOk, UnityAction onCancel)
    {
        this.onOk = onOk;
        this.onCancel = onCancel;
    }

}
