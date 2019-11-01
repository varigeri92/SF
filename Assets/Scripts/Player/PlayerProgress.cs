using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{

	public int powerCoresCollected;
	public int currency;

	public int powerCoresThisLevel;

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
	}

	public void levelCompleted(){
		powerCoresCollected += powerCoresThisLevel;
	}
}
