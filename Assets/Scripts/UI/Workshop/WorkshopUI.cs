using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopUI : MonoBehaviour
{
	public delegate void Upgrade();
	public static event Upgrade OnUpgrade;

	public PlayerObject playerObject;

	public TMPro.TMP_Text cores;
	public TMPro.TMP_Text cores_2;

	public TMPro.TMP_Text level;
	public TMPro.TMP_Text level_2;

    // Start is called before the first frame update
    void Start()
    {
		SetText();
    }

	void SetText(){
		level.text = playerObject.level.ToString();
		level_2.text = playerObject.level.ToString();

		cores.text = playerObject.powerCores.ToString();
		cores_2.text = playerObject.powerCores.ToString();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void AddPowerCores(int cores){
		
	}

	public void Upgraded(){
		if(OnUpgrade != null){
			OnUpgrade();
		}
		SetText();
	}
}
