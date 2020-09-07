using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public Transform gunGrid;

    public Transform ultimateGrid;

	[SerializeField]
	private GameObject _basicGun;

	[SerializeField]
	private GameObject _basicGunIcon;

	private bool initialize = true;

	public bool isMouseoverSlot = false;

	bool isGun = true;

    public Animator piAnimator;
    public bool selectFirstButton;

	// Start is called before the first frame update
	void Start()
    {
        InputManager.OnUltimateButtonPressed += SetPiJoyButtonTrue;
        InputManager.OnUltimateButtonReleased += SetPiJoyButtonFalse;
        InitEquipment();
    }

    private void OnDestroy()
    {
        InputManager.OnUltimateButtonPressed -= SetPiJoyButtonTrue;
        InputManager.OnUltimateButtonReleased -= SetPiJoyButtonFalse;

    }

    void SetPiJoyButtonTrue()
    {
        Debug.Log("Event Trigger_True BOI");
        piUI.joystickButton = true;
    }

    void SetPiJoyButtonFalse()
    {
        Debug.Log("Event Trigger_False BOI");
        piUI.joystickButton = false;
    }

    public void ClearUI()
    {
        int childs = gunGrid.childCount;
        for (int i=0; i<childs; i++ )
        {
            Destroy(gunGrid.GetChild(i).gameObject);
        }

        childs = ultimateGrid.childCount;
        for (int i = 0; i < childs; i++)
        {
            Destroy(ultimateGrid.GetChild(i).gameObject);
        }
    }

    public void InitEquipment()
    {
        selectedSlot = "none";
        lastSelectedSlot = "none";

        SetGun(_basicGun, _basicGunIcon, "One", null);
        initialize = false;
        LoadPlayerInventory();
        LoadUnlockedItems();
    }

    public void OnWorksopOpened()
    {
        ClearUI();
        InitEquipment();
    } 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            piUI.joystickButton = true;
        }
    }
	public void HoverEnter(){
		isMouseoverSlot = true;
	}

	public void HoverExit(){
		isMouseoverSlot = false;
	}

	private void LoadUnlockedItems()
	{
        Debug.Log("Loading Unlocked Gun Items");
		foreach (var element in playerObject.availableGuns) {
            Debug.Log(gunGrid);
			Instantiate(element,gunGrid);
            EquipmentUI elementGui = element.GetComponent<EquipmentUI>();
            if (playerObject.inventoryGuns.Contains(elementGui.gun))
            {
                elementGui.SetMarker(true);
            }
		}

        foreach (var element in playerObject.ultimates)
        {
            Instantiate(element, ultimateGrid);
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
		SetGun(selectedGun, gunIcon, slotName, ammoToSpawn);
        piAnimator.SetBool("Open", false);
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
		Debug.Log("SETGUN BOI!!");
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
	}

    public void SetSelectedGun(GameObject gun, GameObject _gunIcon, GameObject ammo)
    {
        selectedGun = gun;
        gunIcon = _gunIcon;
        ammoToSpawn = ammo;
    }

    public void SetUltimate()
    {
        // NO IDEA
    }

    public void SelectGunButton()
    {
        int childs = gunGrid.childCount;
        for (int i = 0; i<childs; i++)
        {
            if (gunGrid.GetChild(i).gameObject.activeSelf)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gunGrid.GetChild(i).gameObject);
                if (selectFirstButton)
                    return;
            }
        }
    }

    void SetButtonNavigation(Button current, Button previous, Button next)
    {
        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.Explicit;
        nav.selectOnUp = null;
        nav.selectOnDown = null;
        nav.selectOnLeft = previous;
        nav.selectOnRight = next;

        current.navigation = nav;
    }

    void SetButtonNavigation(Button current, Button previous, Button next, Button up, Button down)
    {
        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.Explicit;
        nav.selectOnUp = up;
        nav.selectOnDown = down;
        nav.selectOnLeft = previous;
        nav.selectOnRight = next;

        current.navigation = nav;
    }


    public void SetupNavigationGun()
    {
        int childs = gunGrid.childCount;
        int columns = gunGrid.GetComponent<GridLayoutGroup>().constraintCount;
        for (int i = 0; i < childs; i++)
        {
            if (gunGrid.GetChild(i).gameObject.activeSelf)
            {
                Button button = GetButtonComp(gunGrid.GetChild(i).gameObject);
                if (button != null)
                {
                    if (i == 0)
                    {
                        if (childs < columns)
                        {
                            SetButtonNavigation(button,
                                GetButtonComp(gunGrid.GetChild(childs - 1).gameObject),
                                GetButtonComp(gunGrid.GetChild(i + 1).gameObject),
                                null,
                                SetUltimateButtonToSelect());
                        }
                        else
                        {
                            SetButtonNavigation(button,
                                GetButtonComp(gunGrid.GetChild(childs - 1).gameObject),
                                GetButtonComp(gunGrid.GetChild(i + 1).gameObject),
                                null,
                                GetButtonComp(gunGrid.GetChild(i + columns).gameObject));
                        }
                    }
                    else if( i+1 == childs)
                    {
                        if (i >= columns)
                        {
                            SetButtonNavigation(button,
                            GetButtonComp(gunGrid.GetChild(i - 1).gameObject),
                            GetButtonComp(gunGrid.GetChild(0).gameObject),
                            GetButtonComp(gunGrid.GetChild(i-columns).gameObject),
                            SetUltimateButtonToSelect());
                        }
                        else
                        {
                            SetButtonNavigation(button,
                            GetButtonComp(gunGrid.GetChild(i - 1).gameObject),
                            GetButtonComp(gunGrid.GetChild(0).gameObject),
                            null,
                            SetUltimateButtonToSelect());
                        }
                        
                    }
                    else
                    {
                        SetButtonNavigation(button,
                            GetButtonComp(gunGrid.GetChild(i - 1).gameObject),
                            GetButtonComp(gunGrid.GetChild(i + 1).gameObject),
                            null,
                            SetUltimateButtonToSelect());
                    }
                }
                
            }
        }
    }

    Button GetButtonComp(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return null;
        }
        return gameObject.GetComponent<Button>();
    }

    private void SelectUltimateButton()
    {

    }
    Button SetUltimateButtonToSelect()
    {
        if (ultimateGrid.childCount > 0)
        {
            return ultimateGrid.GetChild(0).GetComponent<Button>();
        }
        else
        {
            return null;
        }
    }
}
