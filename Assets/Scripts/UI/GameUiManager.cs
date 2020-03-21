using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameUiManager : MonoBehaviour
{

	public CanvasGroup completedPanel;
    public GameObject continueButton;
    public GameObject retryButton;


    void OnLevelCompleted(){
		completedPanel.gameObject.SetActive(true);
		StartCoroutine(OpencomplettePanel());
        EventSystem.current.SetSelectedGameObject(continueButton);
	}

    void OnGameOver()
    {
        EventSystem.current.SetSelectedGameObject(retryButton);
    }

	IEnumerator OpencomplettePanel(){
		while(completedPanel.alpha < 1){
			completedPanel.alpha += 0.02f;
			yield return new WaitForSecondsRealtime(0.05f);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		LevelManager.OnLevelCompleted += OnLevelCompleted;
        Player.OnPlayerDeath += OnGameOver;
    }

	private void OnDestroy()
	{
		LevelManager.OnLevelCompleted -= OnLevelCompleted;
        Player.OnPlayerDeath -= OnGameOver;
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
