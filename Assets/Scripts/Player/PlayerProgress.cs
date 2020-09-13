using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{

	public int powerCoresCollected;
	public int currency;

	public int powerCoresThisLevel;

	public PlayerObject playerObject;
	public SaveObject saveObject;
	public PlayerStateObject playerStateObject;

	public delegate void CorePickedUp();
	public static event CorePickedUp OnCorePickedUp;

	private static PlayerProgress instance;
	public static PlayerProgress Instance {
		get { return instance; }
		set { instance = value; }
	}

	private void Awake()
	{
		DontDestroyOnLoad(this);
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
	}

	private void Start()
	{
		SaveOrLoadGameState();

	}


	// REMOVE AT RELEASE:
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C)) {
			Cursor.visible = !Cursor.visible;
		}
	}

	public void SaveOrLoadGameState(){
		/*
		if(SaveManager.Instance == null){
			Debug.Log("SaveManager.Instance is null!!");
		}
		if (SaveManager.Instance.IsSaveExists()) {
			SaveManager.Instance.LoadPlayerState();
		} else {
			SaveManager.Instance.SaveGamePlayerState();
		}
        */
	}

	public void PickUpPowerCore(){
		powerCoresThisLevel++;
		if (OnCorePickedUp != null)
			OnCorePickedUp();
	}

	public void levelCompleted(){
		powerCoresCollected += powerCoresThisLevel;
		playerObject.powerCores += powerCoresThisLevel;
	}
}
