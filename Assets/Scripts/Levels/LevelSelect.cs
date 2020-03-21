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

    GameObject lastAvailableLevelGO;

    public Button onDownButton;
    public Button onUpButton;

    List<Button> levelButtons = new List<Button>();
    [SerializeField]
    List<Button> sideButtons = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
		bool isNextAvailable = true;
        int iterator = 0;
		foreach (LevelObject level in levels){
			//levelPrefab.transform.Find("BG_Image").GetComponent<UnityEngine.UI.Image>().sprite = level.backgroundImage;
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

            GameObject go = Instantiate(levelPrefab, Content.transform);
            if (level.available)
            {
                lastAvailableLevelGO = go;
                levelButtons.Add(go.GetComponent<Button>());
            }
            
            if (!PresistentOptionsManager.Instance.justStarted)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(lastAvailableLevelGO);
            }

        }

        int buttonCount = levelButtons.Count; 
        for (int i = 0; i < buttonCount; i++)
        {
            Navigation buttonNav = new Navigation();
            buttonNav.mode = Navigation.Mode.Explicit;
            buttonNav.selectOnUp = onUpButton;
            buttonNav.selectOnDown = onDownButton;
            if (i == 0)
            {
                buttonNav.selectOnLeft = levelButtons[i];
            }
            else
            {
                buttonNav.selectOnLeft = levelButtons[i-1];
            }
            if (i+1 != buttonCount)
            {
                buttonNav.selectOnRight = levelButtons[i + 1];
            }
            else
            {
                buttonNav.selectOnRight = levelButtons[0];
            }

            levelButtons[i].navigation = buttonNav;
        }

        SetSideButtons();
        
    }

    public void SelectNextLevelButton()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(lastAvailableLevelGO);

    }

    public void SetSideButtons()
    {
        for (int i = 0; i< sideButtons.Count; i++)
        {
            Navigation buttonNav = new Navigation();
            buttonNav.mode = Navigation.Mode.Explicit;
            if (i == 0)
            {
                buttonNav.selectOnUp = sideButtons[sideButtons.Count-1];
            }
            else
            {
                buttonNav.selectOnUp = sideButtons[i - 1];
            }
            if (i + 1 != sideButtons.Count)
            {
                buttonNav.selectOnDown = sideButtons[i + 1];
            }
            else
            {
                buttonNav.selectOnDown = sideButtons[0];
            }

            buttonNav.selectOnLeft = lastAvailableLevelGO.GetComponent<Button>();
            buttonNav.selectOnRight = lastAvailableLevelGO.GetComponent<Button>();
            sideButtons[i].navigation = buttonNav;
        }
    }

    private void OnLevelWasLoaded(int level)
    {

        SelectNextLevelButton();
    }
}
