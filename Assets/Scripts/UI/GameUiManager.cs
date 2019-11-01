using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{

	public CanvasGroup completedPanel;



	void OnLevelCompleted(){
		completedPanel.gameObject.SetActive(true);
		StartCoroutine(OpencomplettePanel());
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
    }

	private void OnDestroy()
	{
		LevelManager.OnLevelCompleted -= OnLevelCompleted;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
