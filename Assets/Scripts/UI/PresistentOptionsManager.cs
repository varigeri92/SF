using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresistentOptionsManager : MonoBehaviour {

	private PresistentOptionsManager instance;
	public Texture2D cursorImg;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	void Awake(){
		Debug.Log("KORREKT!");
		if(instance == null){
			instance = this;
		}else{
			Destroy(this);
		}
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
