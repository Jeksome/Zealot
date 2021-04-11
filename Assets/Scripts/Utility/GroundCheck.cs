using UnityEngine;
using UnityEngine.UI;

public class GroundCheck : MonoBehaviour
{
    public Text state;
    private bool isGrounded;
    private bool cheatsEnabled;

    public void GetStatus(bool groundedStatus) => isGrounded = groundedStatus;
    public void DisplayStatus() => cheatsEnabled = true;
    public void HideStatus() => cheatsEnabled = false;
    private void Update()
    {
        if (cheatsEnabled == true)
        {
            if (isGrounded)
            {
                state.text = "Grounded";
                state.color = Color.green;
            }
            else
            {
                state.text = "In air";
                state.color = Color.red;
            }
        }  
        else
        {
            state.text = "";
        }
    }   
}
