using UnityEngine;

public class MainMenu : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private GameObject optionsMenu;
    #pragma warning restore 0649

    public void StartGame() => GameManager.Instance.TryToLoadLevel("level1");
    public void GameOptions()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }  
    public void Continue() => GameManager.Instance.TogglePause();
    public void Restart() => GameManager.Instance.RestartGame();
    public void ExitGame() => Application.Quit();
}
