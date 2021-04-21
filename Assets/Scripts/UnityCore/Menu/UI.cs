using UnityEngine;

public class UI : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private GameObject userInterfaceWindow;
    [SerializeField] private GameObject bootCamera;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject restartButton;
    #pragma warning restore 0649

    private void Start() => GameManager.Instance.OnGameStateChange.AddListener(HandleGameStateChanged);

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            userInterfaceWindow.SetActive(false);
            bootCamera.SetActive(false);
            startButton.SetActive(false);
            continueButton.SetActive(true);
            restartButton.SetActive(true);
        }
        else if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.PAUSED)
            userInterfaceWindow.SetActive(true);
        else if (previousState == GameManager.GameState.PAUSED && currentState == GameManager.GameState.RUNNING)
            userInterfaceWindow.SetActive(false);
    }
}
