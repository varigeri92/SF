using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopDevController : MonoBehaviour
{
	public PlayerObject referenceObject;
	public PlayerObject playerObject;

	public List<UpgradeButtonObject> perks;

	public void ResetPLayer(){
		playerObject.maxHealth = referenceObject.maxHealth;
		playerObject.level = 1;
		playerObject.speed = referenceObject.speed;
		playerObject.boostFillSpeed = referenceObject.boostFillSpeed;
		playerObject.boostDecSpeed = referenceObject.boostDecSpeed;

		foreach (var perk in perks) {
			// perk.upgraded = false;
		}
	}

	public void AddPowerCores(int cores){
		playerObject.powerCores += cores;
	}

	public void ResetPowerCores()
	{
		playerObject.powerCores = 0;
	}


	// Start called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
