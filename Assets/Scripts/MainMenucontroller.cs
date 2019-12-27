﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenucontroller : MonoBehaviour
{
    [SerializeField]
    GameObject MainButtons;
    [SerializeField]
    GameObject ControllsPanel;
    [SerializeField]
    GameObject ExitPanel;


    public void OpenCloseControlls(bool open)
    {
        if (ControllsPanel.activeSelf)
        {
            ControllsPanel.SetActive(open);
            MainButtons.SetActive(!open);
        }
        else
        {
            ControllsPanel.SetActive(open);
            MainButtons.SetActive(!open);
        }
    }

    public void OpenCloseExitPanel(bool open)
    {
        if (ControllsPanel.activeSelf)
        {
            ExitPanel.SetActive(open);
            MainButtons.SetActive(!open);
        }
        else
        {
            ExitPanel.SetActive(open);
            MainButtons.SetActive(!open);
        }
    }

	public void OpenScene(int sceneIndex){
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
	}

}
