using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCard : MonoBehaviour
{

	public LevelObject level;
	public int index;

	public void SelectLevel()
	{
		if(level.available){
			SelectedLevel.Instance.SetLevel(level);
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
	}
}
