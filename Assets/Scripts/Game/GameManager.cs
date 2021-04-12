using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState  { PREGAME, RUNNING, PAUSED }
    private GameState currentGameState = GameState.PREGAME;
    private AsyncOperation sceneLoadOperation;
    private AsyncOperation loadingScreen;

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

    public void TryToLoadLevel (string levelName)
    {
        loadingScreen = SceneManager.LoadSceneAsync("loading", LoadSceneMode.Additive);
        loadingScreen.completed += LoadingScreenLoaded;
        currentLevel = levelName;
    }

    private void LoadLevel()
    {
        sceneLoadOperation = SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
        sceneLoadOperation.completed += OnLoadOperationComplete;

        UpdateState(GameState.RUNNING);
        UnloadLevel("loading");
    }
   
    public void UnloadLevel (string levelName)
    {      
        AsyncOperation sceneUnloadOperation = SceneManager.UnloadSceneAsync(levelName);
        sceneUnloadOperation.completed += OnUnloadOperationComplete;
    }

    private void LoadingScreenLoaded(AsyncOperation operation) => Invoke("LoadLevel", 2f);
    private void OnLoadOperationComplete(AsyncOperation operation) => UnloadLevel("loading");
    private void OnUnloadOperationComplete(AsyncOperation operation) => Debug.Log("Unload complete");
    public void TogglePause() => UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
        UnloadLevel(currentLevel);
        TryToLoadLevel("level1");
    }
}
