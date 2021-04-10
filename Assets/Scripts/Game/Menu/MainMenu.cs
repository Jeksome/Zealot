using UnityEngine;

public class MainMenu : MonoBehaviour
{  
    public void StartGame() => GameManager.Instance.LoadLevel("level1");
    public void GameOptions() => Debug.Log("No options implemented so far");  
    public void Continue() => GameManager.Instance.TogglePause();
    public void Restart() => GameManager.Instance.RestartGame();
    public void ExitGame() => Application.Quit();
}
