using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIManager : MonoBehaviour
{

	Dictionary<string, GunObject> inventoryGuns = new Dictionary<string, GunObject>();

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
    [SerializeField]
    GunEquipmentButton[] gunButtons;
    [SerializeField]
    UltEquipmentButton[] ultButtons;

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
        gunButtons = gunGrid.GetComponentsInChildren<GunEquipmentButton>();
        SetGunMarkers();
        ultButtons = ultimateGrid.GetComponentsInChildren<UltEquipmentButton>();
        setUltMarkers();
    }

    void SetGunMarkers()
    {
        foreach (GunEquipmentButton gunButton in gunButtons)
        {
            if (Global.Instance.PlayerData.equipedGuns.Contains(gunButton.gunObject))
            {
                gunButton.SetMarker(true);
            }
            else
            {
                gunButton.SetMarker(false);
            }
        }
    }


    void setUltMarkers()
    {
        foreach (UltEquipmentButton ultButton in ultButtons)
        {
            if (Global.Instance.PlayerData.ultimate == ultButton.ultimate)
            {
                ultButton.SetMarker(true);
            }
            else
            {
                ultButton.SetMarker(false);
            }
        }
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



            string slotToRemoveFrom = "";

            foreach (KeyValuePair<string, GunObject> entry in inventoryGuns)
            {
                if (entry.Value == gunObject)
                {
                    slotToRemoveFrom = entry.Key;
                    Destroy(piUIGo.transform.Find(slotToRemoveFrom).GetChild(0).GetChild(0).gameObject);
                    break;
                }
            }
            if(slotToRemoveFrom != "")
            {
                Global.Instance.PlayerData.equipedGuns.Remove(inventoryGuns[slotToRemoveFrom]);
                inventoryGuns.Remove(slotToRemoveFrom);
            }


            if (!inventoryGuns.ContainsKey(slotName))
            {
                inventoryGuns.Add(slotName, gunObject);
                GameObject inventoryIcon = Instantiate(gunObject.gunIcon, piUIGo.transform.Find(slotName).GetChild(0));
            }
            else
            {
                Debug.Log("Slot: " + slotName + " already in use!");
                Destroy(piUIGo.transform.Find(slotName).GetChild(0).GetChild(0).gameObject);
                Global.Instance.PlayerData.equipedGuns.Remove(inventoryGuns[slotName]);
                inventoryGuns.Remove(slotName);

                inventoryGuns.Add(slotName, gunObject);
                Global.Instance.PlayerData.equipedGuns.Add(gunObject);
                GameObject inventoryIcon = Instantiate(gunObject.gunIcon, piUIGo.transform.Find(slotName).GetChild(0));
            }

        }
        else
        {
            if (inventoryGuns.ContainsKey(slotName)) {
                Debug.Log("Slot: " + slotName + " already in use!");

                Destroy(piUIGo.transform.Find(slotName).GetChild(0).GetChild(0).gameObject);
                Global.Instance.PlayerData.equipedGuns.Remove(inventoryGuns[slotName]);
                inventoryGuns.Remove(slotName);

                inventoryGuns.Add(slotName, gunObject);
                Global.Instance.PlayerData.equipedGuns.Add(gunObject);
                GameObject inventoryIcon = Instantiate(gunObject.gunIcon, piUIGo.transform.Find(slotName).GetChild(0));
            }
            else
            {
                Debug.Log("Seting Gun: " + slotName);
				inventoryGuns.Add(slotName, gunObject);
                Global.Instance.PlayerData.equipedGuns.Add(gunObject);
                GameObject inventoryIcon = Instantiate(gunObject.gunIcon, piUIGo.transform.Find(slotName).GetChild(0));
            }
		}

        SetGunMarkers();
    }

    public void SetSelectedGun(GunObject gunObject)
    {
        selectedGun = gunObject;
    }


    public void SetUltimate(Abilit_Object abilityObject)
    {
        Global.Instance.PlayerData.ultimate = abilityObject;
        setUltMarkers();
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
