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
	public TMPro.TMP_Text cores_2;

	public TMPro.TMP_Text level;
	public TMPro.TMP_Text level_2;

	public UnityEngine.UI.Text text;

    // Start is called before the first frame update
    void Start()
    {
		SetText();
		//PlayerProgress.Instance.SetPerks(ref buttonObjects);
		text.text = "FILEPATH: " + SaveManager.Instance.GetFilePath();
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
		SaveManager.Instance.SavePerks(buttonObjects);
	}

	public void UpgradeDone(int sceneIndex){
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
	}


}
