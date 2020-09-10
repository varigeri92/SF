using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopUI : MonoBehaviour
{
	public delegate void Upgrade();
	public static event Upgrade OnUpgrade;

	public TMPro.TMP_Text cores;
	public TMPro.TMP_Text level;


    // Start is called before the first frame update
    void Start()
    {
		SetText();
    }

	void SetText(){
		level.text = Global.Instance.PlayerData.level.ToString();
		cores.text = Global.Instance.PlayerData.powerCores.ToString();
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
