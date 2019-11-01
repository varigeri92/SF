using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCard : MonoBehaviour
{

	public LevelObject level;

	public void SelectLevel()
	{
		SelectedLevel.Instance.SetLevel(level);
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}
}
