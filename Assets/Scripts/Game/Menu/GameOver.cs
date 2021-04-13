using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Restart() => GameManager.Instance.RestartGame();
    public void ExitGame() => Application.Quit();
}
