using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsDisplay;
    private bool cheatsEnabled;

    public void StartFrameCounter() => cheatsEnabled = true;
    public void StopFrameCounter() => cheatsEnabled = false;

    private void Update()
    {
        if (cheatsEnabled == true)
        {
            int fps = (int)(1 / Time.unscaledDeltaTime);
            fpsDisplay.text = "FPS:" + fps;
        }
        else fpsDisplay.text = "";
    }
}
