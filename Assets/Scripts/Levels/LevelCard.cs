using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCard : MonoBehaviour
{

	public LevelObject level;
	public int index;
    public TMPro.TMP_Text text;

    public void SetText(string s)
    {
        text.text = s;
    }

    public void SelectLevel()
	{
		if(level.available){
			SelectedLevel.Instance.SetLevel(level);
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
	}
}
