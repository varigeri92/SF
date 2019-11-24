using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{


	public List<LevelObject> levels = new List<LevelObject>();

	public GameObject levelPrefab;
	public GameObject Content;

	public Color lockedColor;
	public Color availableColor;
	public Color completteColor;

	public TMPro.TMP_Text Text;
	public Image backGroundPreview;

    // Start is called before the first frame update
    void Start()
    {
		bool isNextAvailable = true;
		foreach (LevelObject level in levels){
			levelPrefab.transform.Find("BG_Image").GetComponent<UnityEngine.UI.Image>().sprite = level.backgroundImage;
			levelPrefab.GetComponent<LevelCard>().level = level;
			Image levelImage = levelPrefab.GetComponent<Image>();
			levelImage.color = lockedColor;

			if(isNextAvailable){
				level.available = true;
				isNextAvailable = false;
			}

			if(level.available){
				levelImage.color = availableColor;
			}

			if(level.completed){
				levelImage.color = completteColor;
				isNextAvailable = true;
			}

			Instantiate(levelPrefab, Content.transform);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
