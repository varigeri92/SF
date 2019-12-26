using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUIManager : MonoBehaviour
{
	public PlayerObject playerObject;

	public List<GameObject> guns = new List<GameObject>();
	public List<GameObject> icons = new List<GameObject>();
	public List<GameObject> ammos = new List<GameObject>();

	Dictionary<string, GameObject> inventoryGuns = new Dictionary<string,GameObject>();
	Dictionary<string, GameObject> inventoryIcons = new Dictionary<string, GameObject>();

	public List<string> slots;
	string lastSelectedSlot;
	public string selectedSlot;

	public PiUI piUI;
	public PiPiece piece;

	public GameObject selectedGun;
	public GameObject gunIcon;
	public GameObject ammoToSpawn;

	public GameObject piUIGo;

	public Transform grid;

	[SerializeField]
	private GameObject _basicGun;

	[SerializeField]
	private GameObject _basicGunIcon;

	private bool initialize = true;

	public bool isMouseoverSlot = false;

	bool isGun = true;

	// Start is called before the first frame update
	void Start()
    {
		selectedSlot = "none";
		lastSelectedSlot = "none";

		SetGun(_basicGun,_basicGunIcon,"One", null);
		initialize = false;
		LoadPlayerInventory();
		LoadUnlockedItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void HoverEnter(){
		isMouseoverSlot = true;
	}

	public void HoverExit(){
		isMouseoverSlot = false;
	}

	private void LoadUnlockedItems()
	{
		foreach (var element in playerObject.availableGuns) {
			Instantiate(element,grid);
		}
	}

	public void BeginDrag(GameObject gun, GameObject icon, GameObject _ammoToSpawn, bool _isGun){
		Debug.Log("Hello!");
		selectedGun = gun;
		gunIcon = icon;
		ammoToSpawn = _ammoToSpawn;
		isGun = _isGun;
	}

	public void CancelDrag(){
		selectedGun = null;
		gunIcon = null;
	}

	public void SelectSlot(string slotName){
		isMouseoverSlot = true;
		SetGun(selectedGun, gunIcon, slotName, ammoToSpawn);

	}

	public void DeselectSlot(){
		selectedSlot = "none";
	}

	public void LoadPlayerInventory()
	{
		for (int i = 1; i < playerObject.inventoryGuns.Count; i++){
			SetGun(playerObject.inventoryGuns[i],playerObject.inventoryIcons[i],slots[i], playerObject.AmmoToSpawn[i]);
		}

	}


	public void SetGun(GameObject gun, GameObject icon, string slotName, GameObject _ammoToSpawn)
	{
		if(gun == null){
			return;
		}
		if(!isGun){
			return;
		}
		if(slotName == "One" && !initialize){
			Debug.Log("Cannot modify this slot!");
			return;
		}

		if(guns.Contains(gun)){
			Debug.Log(gun.name + "Already in the inventory!");

			guns.Remove(gun);
			icons.Remove(icon);
			ammos.Remove(_ammoToSpawn);

			string slot = "";

			foreach(KeyValuePair<string,GameObject> entry in inventoryGuns){
				if(entry.Value == gun){
					Debug.Log(entry.Key + " -----> " + entry.Value.name);
					slot = entry.Key;
					Destroy(piUIGo.transform.Find(slot).GetChild(0).GetChild(0).gameObject);
					inventoryGuns.Remove(slot);
					if (inventoryGuns.ContainsKey(slotName)){
						Debug.Log("Slot: " + slotName + " already in use!");
						int index = guns.IndexOf(inventoryGuns[slotName]);
						guns.RemoveAt(index);
						icons.RemoveAt(index);
						ammos.RemoveAt(index);
						Destroy(piUIGo.transform.Find(slotName).GetChild(0).GetChild(0).gameObject);
						inventoryGuns.Remove(slotName);
					}
					inventoryGuns.Add(slotName, gun);

					guns.Add(gun);
					icons.Add(icon);
					ammos.Add(_ammoToSpawn);
					break;
				}
			}
		}else{
			guns.Add(gun);
			icons.Add(icon);
			ammos.Add(_ammoToSpawn);
			if(inventoryGuns.ContainsKey(slotName)){
				Debug.Log("Slot: " + slotName + " already in use!");
				int index = guns.IndexOf(inventoryGuns[slotName]);
				guns.RemoveAt(index);
				icons.RemoveAt(index);
				ammos.RemoveAt(index);
				Destroy(piUIGo.transform.Find(slotName).GetChild(0).GetChild(0).gameObject);
				inventoryGuns.Remove(slotName);
				inventoryGuns.Add(slotName, gun);
			}else{
				inventoryGuns.Add(slotName, gun);
			}
		}
		GameObject inventoryIcon = Instantiate(icon, piUIGo.transform.Find(slotName).GetChild(0));
	}

	public void SaveInventory()
	{
		playerObject.inventoryGuns = new List<GameObject>(guns);
		playerObject.inventoryIcons = new List<GameObject>(icons);
		playerObject.AmmoToSpawn = new List<GameObject>(ammos);

		SaveManager.Instance.SetEquipedGunsToSave(guns);

		UnityEngine.SceneManagement.SceneManager.LoadScene(2);
	}

}
