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
    private string previousLevel = string.Empty;

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

    public void GameOver()
    {
        previousLevel = currentLevel;
        Time.timeScale = 0;       
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);       
        currentLevel = "GameOver";
        if (previousLevel != null)
        {
            UnloadLevel(previousLevel);
            previousLevel = string.Empty;
        }
        CursorLocker.UnlockCursor();
    }

    public void UnloadLevel(string levelName) => SceneManager.UnloadSceneAsync(levelName);
    private void LoadingScreenLoaded(AsyncOperation operation) => Invoke("LoadLevel", 0.1f);
    private void OnLoadOperationComplete(AsyncOperation operation) => UnloadLevel("loading");
    public void TogglePause() => UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);        
        UnloadLevel(currentLevel);       
        TryToLoadLevel("level1");
    }
}
