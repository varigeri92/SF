using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresistentOptionsManager : MonoBehaviour {

	public Texture2D cursorImg;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	private static PresistentOptionsManager instance;

    public bool justStarted = true;
    public bool survival = false;

    public IntroText introText;
    public CanvasGroup introGroup;

    public CanvasGroup buttonsGroup;
    public UIPanel buttonsPanel;

    public CanvasGroup levelSelectGroup;
    public UIPanel levelSelectPanel;

    public List<string> buttonName;
    public List<Sprite> buttonSprites;


	public static PresistentOptionsManager Instance {
		get {
			return instance;
		}

		set {
			instance = value;
		}
	}

	void Awake(){
		if(Instance == null){
			Instance = this;
		}else if(Instance != this){
			Destroy(this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		Cursor.SetCursor(cursorImg, hotSpot, cursorMode);
	}


    //DONT USE IT!!
    void AutoOpenLevelSelect()
    {
        introGroup = GameObject.FindGameObjectWithTag("IntroText").GetComponent<CanvasGroup>();
        introText = introGroup.GetComponent<IntroText>();
        introText.DisableAnimation();

        buttonsGroup = GameObject.FindGameObjectWithTag("MainMenuButtons").GetComponent<CanvasGroup>();
        buttonsPanel = buttonsGroup.GetComponent<UIPanel>();
        buttonsPanel.Hide();

        levelSelectPanel = GameObject.FindGameObjectWithTag("LevelSelectionPanel").GetComponent<UIPanel>();
        levelSelectPanel.Show();
        levelSelectPanel.SetFocus(true);
        // levelSelectGroup.alpha = 1;
        // levelSelectGroup.blocksRaycasts = true;
    }

}
