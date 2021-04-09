using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.LoadLevel("Level1");
    }

    public void GameOptions()
    {
        Debug.Log("You have no options so far");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
