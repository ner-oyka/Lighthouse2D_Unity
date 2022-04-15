using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAsyncManager : MonoBehaviour
{
    public string levelName;
    List<string> levels = new List<string>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        levels.Add(arg0.name);
    }

    public void LoadTestLevel()
    {
        if (levels.Contains(levelName))
        {
            SceneManager.UnloadSceneAsync(levelName);
            levels.Remove(levelName);
            return;
        }
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
    }

}
