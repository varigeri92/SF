using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIManager : MonoBehaviour
{


	// public List<GameObject> guns = new List<GameObject>();
	// public List<GameObject> icons = new List<GameObject>();
	// public List<GameObject> ammos = new List<GameObject>();

	Dictionary<string, GunObject> inventoryGuns = new Dictionary<string, GunObject>();
	Dictionary<string, GameObject> inventoryIcons = new Dictionary<string, GameObject>();

	public List<string> slots;
	string lastSelectedSlot;
	public string selectedSlot;

	public PiUI piUI;
	public PiPiece piece;

	public GunObject selectedGun;

	public GameObject piUIGo;

	public Transform gunGrid;

    public Transform ultimateGrid;

	[SerializeField]
	private GunObject _basicGun;

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

        SetGun(_basicGun, "One");
        initialize = false;
        LoadPlayerInventory();
        LoadUnlockedItems();
    }

    public void OnWorksopOpened()
    {
        ClearUI();
        InitEquipment();
    } 

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            piUI.joystickButton = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            piUI.CloseMenu();
            piAnimator.SetBool("Open", false);
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
		foreach (GunObject element in Global.Instance.PlayerData.availableGuns) {
			Instantiate(element.equipmentGridPrefab, gunGrid);
            /*
            EquipmentUI elementGui = element.GetComponent<EquipmentUI>();
            if (playerObject.inventoryGuns.Contains(elementGui.gun))
            {
                elementGui.SetMarker(true);
            }
            */
		}

        foreach (Abilit_Object element in Global.Instance.PlayerData.unlockedUltimates)
        {
            Instantiate(element.equipmentGridPrefab, ultimateGrid);
        }
    }


	public void SelectSlot(string slotName){
        Debug.Log("SELECT SLOT");
		SetGun(selectedGun, slotName );
        piAnimator.SetBool("Open", false);
	}

	public void DeselectSlot(){
		selectedSlot = "none";
	}

	public void LoadPlayerInventory()
	{
		for (int i = 1; i < Global.Instance.PlayerData.equipedGuns.Count; i++){
			SetGun(Global.Instance.PlayerData.equipedGuns[i], slots[i] );
		}

	}


	public void SetGun(GunObject gunObject, string slotName)
	{
		Debug.Log("SETGUN BOI!!");
		if(!gunObject) return;
		
		if(!isGun){
            Debug.Log("Isgun False!");
		}

		if(slotName == "One" && !initialize){
			Debug.Log("Cannot modify this slot!");
			return;
		}

		if(Global.Instance.PlayerData.equipedGuns.Contains(gunObject)){
			Debug.Log(gunObject.name + "Already in the inventory!");
            
            // Remove from player inventory!


			string slot = "";

			foreach(KeyValuePair<string, GunObject> entry in inventoryGuns){
				if(entry.Value == gunObject){
					Debug.Log(entry.Key + " -----> " + entry.Value.name);
					slot = entry.Key;
					Destroy(piUIGo.transform.Find(slot).GetChild(0).GetChild(0).gameObject);
					inventoryGuns.Remove(slot);
					if (inventoryGuns.ContainsKey(slotName)){
						Debug.Log("Slot: " + slotName + " already in use!");
						// int index = guns.IndexOf(inventoryGuns[slotName]);
						Destroy(piUIGo.transform.Find(slotName).GetChild(0).GetChild(0).gameObject);
						inventoryGuns.Remove(slotName);
					}
					inventoryGuns.Add(slotName, gunObject);
					break;
				}
			}
		}else{
			if(inventoryGuns.ContainsKey(slotName)){
				Debug.Log("Slot: " + slotName + " already in use!");
				// int index = guns.IndexOf(inventoryGuns[slotName]);
				
				Destroy(piUIGo.transform.Find(slotName).GetChild(0).GetChild(0).gameObject);
				inventoryGuns.Remove(slotName);
				inventoryGuns.Add(slotName, gunObject);
			}else{
				inventoryGuns.Add(slotName, gunObject);
                Global.Instance.PlayerData.equipedGuns.Add(gunObject);
			}
		}
        GameObject inventoryIcon = Instantiate(gunObject.gunIcon, piUIGo.transform.Find(slotName).GetChild(0));
	}

    public void SetSelectedGun(GunObject gunObject)
    {
        selectedGun = gunObject;
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
                            if(childs == 1)
                            {
                                SetButtonNavigation(button,
                                GetButtonComp(gunGrid.GetChild(childs - 1).gameObject),
                                GetButtonComp(gunGrid.GetChild(i).gameObject),
                                null,
                                SetUltimateButtonToSelect());
                            }
                                else
                            {
                                SetButtonNavigation(button,
                                    GetButtonComp(gunGrid.GetChild(childs - 1).gameObject),
                                    GetButtonComp(gunGrid.GetChild(i + 1).gameObject),
                                    null,
                                    SetUltimateButtonToSelect());
                            }

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
