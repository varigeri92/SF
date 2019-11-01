using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUIManager : MonoBehaviour
{

	public List<string> slots;
	string lastSelectedSlot;
	public string selectedSlot;

	public PiUI piUI;
	public PiPiece piece;


    // Start is called before the first frame update
    void Start()
    {
		selectedSlot = "none";
		lastSelectedSlot = "none";



    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SelectSlot(string slotName){
		Debug.Log(slotName);
		selectedSlot = slotName;

	}

	public void DeselectSlot(){
		selectedSlot = "none";
	}

}
