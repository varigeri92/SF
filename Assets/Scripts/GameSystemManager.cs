﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameSystemManager : MonoBehaviour
{

    TimeManager timeManager;
    public GameObject pauseMenu;
    public GameObject continueButton;
    public bool isPaused { get; private set; }

    public GameObject ContinueButton;


    void Start()
    {
        isPaused = false;
        timeManager = GetComponentInChildren<TimeManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("XONE_MENU") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame(true);
                EventSystem.current.SetSelectedGameObject(continueButton);
            }
            else
            {
                PauseGame(false);
                EventSystem.current.SetSelectedGameObject(null);
            }

        }
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;
        pauseMenu.SetActive(pause);
        if (pause)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(ContinueButton);
            timeManager.Pause();
        }
        else
        {
            timeManager.NormalizeTime();
        }
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
		SceneManager.LoadScene(0);
	}

    public void Close()
    {
        Application.Quit();
    }

}
