using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{

	public int powerCoresCollected;
	public int currency;

	public int powerCoresThisLevel;

	public PlayerObject playerObject;

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
