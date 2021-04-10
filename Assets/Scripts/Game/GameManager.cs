using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState  { PREGAME, RUNNING, PAUSED }
    private GameState currentGameState = GameState.PREGAME;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }
      
    public Events.EventGameState OnGameStateChange;

    private string currentLevel = string.Empty;

    private void Start() => DontDestroyOnLoad(gameObject);

    private void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;       

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1f;
                CursorLocker.LockCursor();
                break;
            case GameState.RUNNING:
                Time.timeScale = 1f;
                CursorLocker.LockCursor();
                break;
            case GameState.PAUSED:
                Time.timeScale = 0;
                CursorLocker.UnlockCursor();
                break;
        }

        OnGameStateChange.Invoke(currentGameState, previousGameState);
    }

    public void LoadLevel (string levelName)
    {
        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        
        if (sceneLoadOperation == null)
        {
            Debug.Log("[Game Manager] unable to load the level" + levelName);
        }

        currentLevel = levelName;

        UpdateState(GameState.RUNNING);
        Debug.Log($"State = {currentGameState}");
    }
   
    public void UnloadLevel (string levelName)
    {
        AsyncOperation sceneUnloadOperation = SceneManager.UnloadSceneAsync(levelName);
        if ( sceneUnloadOperation  ==  null)
        {
            Debug.Log("[Game Manager] unable to unload the level" + levelName);
        }
        sceneUnloadOperation.completed += OnUnloadOperationComplete;
    }

    private void OnUnloadOperationComplete(AsyncOperation operation) => Debug.Log("Unload complete");
    public void TogglePause() => UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
        UnloadLevel(currentLevel);
        LoadLevel("level1");
    }
}
