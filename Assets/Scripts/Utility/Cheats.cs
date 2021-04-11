using System.Collections;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public PlayerCharacter player;
    public Staff staff;
    public FPSCounter frameCounter;
    public GroundCheck groundCheck;
    private bool cheatsEnabled;

    private void Update()
    {       
        if (Input.GetKeyDown(KeyCode.U) && cheatsEnabled == false)
        {          
            staff.gameObject.SetActive(true);           
            frameCounter.StartFrameCounter();
            groundCheck.DisplayStatus();
            cheatsEnabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.U) && cheatsEnabled == true)
        {
            groundCheck.HideStatus();
            frameCounter.StopFrameCounter();
            cheatsEnabled = false; 
        }

        if (cheatsEnabled == true)
        {
            StartCoroutine(IncreaseHealth());
        }
    }   

    private IEnumerator IncreaseHealth()
    {
        player.AddArmor(100);
        player.Heal(100);
        yield return new WaitForSeconds(0.1f);           
    }
}
