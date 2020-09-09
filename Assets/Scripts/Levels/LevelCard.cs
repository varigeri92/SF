using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{

	public LevelObject level;
	public int index;
    [SerializeField] TMP_Text text;
    Button button;


    public void InitButton()
    {
        button = GetComponent<Button>();
        if (level.available)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void SetText(string s)
    {
        text.text = s;
    }

    public void SelectLevel()
	{
		if(level.available){
            PresistentOptionsManager.Instance.survival = false;
            SelectedLevel.Instance.SetLevel(level);
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
	}
}
