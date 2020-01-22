using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresistentOptionsManager : MonoBehaviour {

	public Texture2D cursorImg;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	private static PresistentOptionsManager instance;

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
        InputManager.OnStart();
	}
	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad(this.gameObject);
		Cursor.SetCursor(cursorImg, hotSpot, cursorMode);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
