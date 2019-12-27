using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
	public string filePath = "";
	public SaveObject saveObject;
	public SaveObject saveObject2;

	public SaveObject loadObject;

	public PlayerObject playerObject;

	public PlayerStateObject playerStateObject;

	public List<UpgradeButtonObject> AllGamePerks;

	bool isFirstStart = true;
	//SINGLETON
	private static SaveManager instance;
	public static SaveManager Instance {
		get { return instance; }
		set { instance = value; }
	}
	//SINGLETON
	private void Awake()
	{
		DontDestroyOnLoad(this);
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
			return;
		}

		if(IsSaveExists()){
			isFirstStart = false;
		}

		InitSavepath();
	}

	private void Start()
	{
		InitIndexes();

	}
	void InitIndexes(){
		int i = 0;
		foreach(PlayerGun playerGun in playerStateObject.allPlayerGuns){
			playerGun.gunObject.index = i;
			playerGun.serializableGun.index = i;
			i++;
		}
		i = 0;
		foreach (PlayerUltimate playerUlt in playerStateObject.allPlayerUltimates) {
			playerUlt.ultObject.index = i;
			i++;
		}
	}

	public void AddAvailableGun(int index){
		if(!playerStateObject.availablePlayerGuns.Contains(index)){
			playerStateObject.availablePlayerGuns.Add(index);
		}
	}

	public void AddEquipedGun(int index){
		if (!playerStateObject.equipedPlayerGuns.Contains(index)) {
			playerStateObject.equipedPlayerGuns.Add(index);
		}
	}

	public void AddAvailableUlt(int index)
	{
		if (!playerStateObject.availablePlayerUltimates.Contains(index)) {
			playerStateObject.availablePlayerUltimates.Add(index);
		}
	}

	public void AddEquipedUlt(int index)
	{
		playerStateObject.equipedPlayerUltimate = index;
	}

	//index needs to be above 0
	// 0 is always set to the basic Gun!
	public void SetEquippedGun(int index)
	{
		playerObject.inventoryGuns.Add(playerStateObject.allPlayerGuns[index].gunPrefab);
		playerObject.inventoryIcons.Add(playerStateObject.allPlayerGuns[index].gunIcon);
	}

	public void SetEquipedGunsToSave(List<GameObject> gunPrefabs){
		List<int> EquippedGunIndexes = new List<int>();
		EquippedGunIndexes.Add(0);
		foreach(GameObject gunPrefab in gunPrefabs){
			for (int i = 1; i < playerStateObject.allPlayerGuns.Count; i++) {
				if (gunPrefab == playerStateObject.allPlayerGuns[i].gunPrefab) {
					Debug.Log("Equiped Gun Saved: " + playerStateObject.allPlayerGuns[i].gunPrefab.name);
					EquippedGunIndexes.Add(playerStateObject.allPlayerGuns[i].gunObject.index);
				}
			}
		}
		saveObject.equipedGunIndexes = EquippedGunIndexes.ToArray();
		SaveGamePlayerState(false); //Serialize and Save the Scriptable Object Without seting Any Other Property.

	}

	public void SetUnlockedGun(int index)
	{
		playerObject.availableGuns.Add(playerStateObject.allPlayerGuns[index].gunGridElement);
	}

	public void ResetEquipedGuns()
	{
		playerObject.inventoryGuns = new List<GameObject>();
		playerObject.inventoryIcons = new List<GameObject>();

		playerObject.inventoryGuns.Add(playerStateObject.allPlayerGuns[0].gunPrefab);
		playerObject.inventoryIcons.Add(playerStateObject.allPlayerGuns[0].gunIcon);
	}



	public void SetEquipedultimate(){
		
	}

	public  void InitSavepath()
	{
		filePath = Application.persistentDataPath + "/Saveplayer.json";
	}

	public  bool IsSaveobjectExists()
	{
		if (saveObject == null) {
			return false;
		}
		return true;
	}

	bool CheckSaveFile()
	{

		if (filePath == "") {
			InitSavepath();
		}

		if (File.Exists(filePath)) {
			return true;
		} else {
			return false;
		}
	}

	public  void SaveGamePlayerState()
	{
		Debug.Log("SAVING PLAYER STATE!");
		SetSaveObject();
		if (CheckSaveFile()) {
			string Json = JsonUtility.ToJson(saveObject);
			File.WriteAllText(filePath, Json);
		}else{
			//File.Create(filePath);
			string Json = JsonUtility.ToJson(saveObject);
			File.WriteAllText(filePath, Json);
		}
	}

	public void SaveGamePlayerState(bool setSaveObject)
	{
		Debug.Log("SAVING PLAYER STATE!");
		if(setSaveObject){
			SetSaveObject();
		}
		if (CheckSaveFile()) {
			string Json = JsonUtility.ToJson(saveObject);
			File.WriteAllText(filePath, Json);
		} else {
			//File.Create(filePath);
			string Json = JsonUtility.ToJson(saveObject);
			File.WriteAllText(filePath, Json);
		}
	}

	/**summary
	 * DONT USE THIS FUNCTION IS ONLY FOR DEBUGING THE BUILD!
	 */
	public  void SaveGamePlayerState(UnityEngine.UI.Text text)
	{
		text.text += "---- \n";
		SetSaveObject();
		text.text += "---- \n";
		if (CheckSaveFile()) {
			text.text += "File exists \n";
			string Json = JsonUtility.ToJson(saveObject);
			File.WriteAllText(filePath, Json);
			text.text = "File exists -> Content Writen! \n";
		} else {
			text.text += "creating save file...  \n";
			File.Create(filePath);
			text.text += "File Created! \n";
		}
	}

	void SetGunObjectsToSave(){
		List<SerializableGunObject> saveGuns = new List<SerializableGunObject>();
		foreach(PlayerGun playerGun in playerStateObject.allPlayerGuns){
			playerGun.serializableGun = new SerializableGunObject(playerGun.gunObject);
			saveGuns.Add(playerGun.serializableGun);
		}
		saveObject.allGunObject = saveGuns.ToArray();
	}

	void LoadGunObjects(){
		foreach(SerializableGunObject serializedGun in saveObject.allGunObject){
			playerStateObject.allPlayerGuns[serializedGun.index].gunObject.damage = serializedGun.damage;
			playerStateObject.allPlayerGuns[serializedGun.index].gunObject.maxAmmo = serializedGun.maxAmmo;
			playerStateObject.allPlayerGuns[serializedGun.index].gunObject.startingAmmo = serializedGun.startingAmmo;
			playerStateObject.allPlayerGuns[serializedGun.index].gunObject.fireRate = serializedGun.fireRate;
			playerStateObject.allPlayerGuns[serializedGun.index].gunObject.index = serializedGun.index;
		}
	}

	public void SetSaveObject()
	{
		if (playerObject == null) {
			Debug.Log("NULL BOI!");
		}
		if(saveObject == null){
			Debug.Log("SaveObject IS null");
			saveObject = saveObject2;
			if (saveObject == null) {
				Debug.Log("SaveObject Still null FUCK BOI!");

			}
		}

		saveObject.plevel = playerObject.level;
		saveObject.pmaxHealth = playerObject.maxHealth;
		saveObject.ppowerCores = playerObject.powerCores;
		saveObject.pboostDecSpeed = playerObject.boostDecSpeed;
		saveObject.pboostFillSpeed = playerObject.boostFillSpeed;
		saveObject.pspeed = playerObject.speed;

		saveObject.equipedGunIndexes = playerStateObject.equipedPlayerGuns.ToArray();
		saveObject.unlockedGunIndexes = playerStateObject.availablePlayerGuns.ToArray();
		saveObject.selectedUltimate = playerStateObject.equipedPlayerUltimate;
		saveObject.ulnlockedUltimateIndexes = playerStateObject.availablePlayerUltimates.ToArray();

		saveObject.lastCompletedLevel = -1;
		Debug.Log("Saving Level: Index: " + SelectedLevel.Instance.levels.Length.ToString());
		for (int i = 0; i < SelectedLevel.Instance.levels.Length; i++){
			Debug.Log("Saving Level: Index: "+i.ToString());
			if(SelectedLevel.Instance.levels[i].completed == false){
				Debug.Log("Level: " + i.ToString() + "is Incomplette!");
				break;
			}
			saveObject.lastCompletedLevel = i;
		}

		SetGunObjectsToSave();

		//saveObject.perks = new PerkUpgradeSave[0];
	}

	public  void SavePerks(List<UpgradeButtonObject> buttonObjects)
	{

		List<PerkUpgradeSave> perkList = new List<PerkUpgradeSave>();
		for (int i = 0; i < buttonObjects.Count; i++) {
			PerkUpgradeSave perk = new PerkUpgradeSave(i, buttonObjects[i].upgraded);
			perkList.Add(perk);
		}
		saveObject.perks = perkList.ToArray();
		SaveGamePlayerState();
	}

	public  void LoadPlayerState()
	{
		Debug.Log("LOADING PLAYER! ... ");
		if (CheckSaveFile()) {
			string Json = File.ReadAllText(filePath);
			if (saveObject != null){
				JsonUtility.FromJsonOverwrite(Json, saveObject);


				playerObject.level 			= 	saveObject.plevel;
				playerObject.maxHealth 		= 	saveObject.pmaxHealth;
				playerObject.powerCores 	= 	saveObject.ppowerCores;
				playerObject.boostDecSpeed 	= 	saveObject.pboostDecSpeed;
				playerObject.boostFillSpeed = 	saveObject.pboostFillSpeed;
				playerObject.speed 			= 	saveObject.pspeed; 

				ResetEquipedGuns();

				foreach(int index in saveObject.equipedGunIndexes){
					if(index!=0)
						SetEquippedGun(index);
				}

				foreach (int index in saveObject.unlockedGunIndexes) {
					if(index != 0)
						SetUnlockedGun(index);
				}

				foreach(PerkUpgradeSave perk in saveObject.perks){
					AllGamePerks[perk.index].upgraded = perk.value;
				}

				if(saveObject.lastCompletedLevel > -1){
					for (int i = 0; i < saveObject.lastCompletedLevel+1; i++){
						SelectedLevel.Instance.levels[i].completed = true;
						SelectedLevel.Instance.levels[i].available = true;
						SelectedLevel.Instance.levels[i+1].available = true;
					}
				}else{
					// SelectedLevel.Instance.levels[0].available = true;
				}

				LoadGunObjects();

				/**
				 * TODO:
					* ULTIMATES
					* GUN Objects!
				*/



			}else{
				throw new Exception("LoadObject is null");
			}
		}

	}
	/**summary
	 * DONT USE THIS FUNCTION IS ONLY FOR DEBUGING THE BUILD!
	 */
	public  void LoadPlayerState(UnityEngine.UI.Text text)
	{
		text.text += "Checking file \n";
		if (CheckSaveFile()) {
			text.text += "File Existing! Reading... \n";
			string Json = File.ReadAllText(filePath);
			text.text += "File Read! \n";
			if (saveObject != null) {
				text.text += "Save object existing! \n";
				JsonUtility.FromJsonOverwrite(Json, saveObject);
				text.text += "Save object is overwriten! \n";
			} else {
				text.text += "saveObject is null \n";
				throw new Exception("saveObject is null");
			}
		}else{
			text.text += "save file not found \n";
		}
	}

	public  bool IsSaveExists()
	{
		if (File.Exists(filePath))
			return true;
		else
			return false;
	}

	public  string GetFilePath()
	{
		if(filePath == ""){
			InitSavepath();
		}
		return filePath;
	}
}

