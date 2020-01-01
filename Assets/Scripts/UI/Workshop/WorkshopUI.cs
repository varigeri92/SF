using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopUI : MonoBehaviour
{
	public delegate void Upgrade();
	public static event Upgrade OnUpgrade;

	public PlayerObject playerObject;
	public List<UpgradeButtonObject> buttonObjects;

	public TMPro.TMP_Text cores;

	public TMPro.TMP_Text level;


    // Start is called before the first frame update
    void Start()
    {
		SetText();
		//PlayerProgress.Instance.SetPerks(ref buttonObjects);
    }

	void SetText(){
		level.text = playerObject.level.ToString();
		cores.text = playerObject.powerCores.ToString();
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
		SaveManager.Instance.SavePerks(buttonObjects);
	}

	public void UpgradeDone(int sceneIndex){
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
	}


}
