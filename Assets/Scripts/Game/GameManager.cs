using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private string currentLevel = string.Empty;
    private List<AsyncOperation> loadOperations;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOperations = new List<AsyncOperation>();
        //LoadLevel("Level1");
    }

    public void LoadLevel (string levelName)
    {
        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(levelName);
        
        if (sceneLoadOperation == null)
        {
            Debug.Log("[Game Manager] unable to load the level" + levelName);
        }

        currentLevel = levelName;
        sceneLoadOperation.completed += OnLoadOperationComplete;    
    }

    private void OnLoadOperationComplete(AsyncOperation operation)
    {
        if (loadOperations.Contains(operation))
        {
            loadOperations.Remove(operation);
        }
        Debug.Log("Load complete");
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

    private void OnUnloadOperationComplete(AsyncOperation operation)
    {
        Debug.Log("Unload complete");
    }
}
