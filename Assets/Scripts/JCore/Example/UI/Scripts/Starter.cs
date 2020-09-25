using JCore.UI;
using UnityEngine;
public class Starter : MonoBehaviour
{
    void Start()
    {
        ScreenManager.Instance.StartNewQueue("Screen1", new FirstScreenParams("A param string", 1098768));
    }
}
