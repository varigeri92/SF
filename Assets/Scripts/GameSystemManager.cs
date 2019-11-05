using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystemManager : MonoBehaviour
{

    TimeManager timeManager;
    public GameObject pauseMenu;
    public bool isPaused { get; private set; }
    
    void Start()
    {
        isPaused = false;
        timeManager = GetComponentInChildren<TimeManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame(true);
            }
            else
            {
                PauseGame(false);
            }

        }
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;
        pauseMenu.SetActive(pause);
        if (pause)
            timeManager.Pause();
        else
            timeManager.NormalizeTime();
    }

    public void ReloadGameScene()
    {
        timeManager.NormalizeTime();
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu()
    {
        timeManager.NormalizeTime();
        SceneManager.LoadScene(0);
    }

	public void LoadLevelSelection()
	{
		timeManager.NormalizeTime();
		SceneManager.LoadScene(2);
	}

    public void Close()
    {
        Application.Quit();
    }

}
