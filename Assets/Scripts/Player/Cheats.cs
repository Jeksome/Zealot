using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public PlayerCharacter player;
    public Staff staff;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Cheats enabled");
            player.Heal(100);
            staff.gameObject.SetActive(true);
        }
    }
}
