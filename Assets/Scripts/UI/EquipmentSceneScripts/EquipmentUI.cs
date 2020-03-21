using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentUI : MonoBehaviour
{

	public bool isGun = true;
	[HideInInspector]
	public bool isDragged = false;
	[HideInInspector]
	public Transform canvas;
	[HideInInspector]
	public EquipmentUIManager equipmentUIManager;

	public UltimateSelector ultimateSelector;
    public UltimateSelector gunSelector;


    Animator animator;

    public GameObject icon;

	[Header("Asing by 'Guns' only:")]
	public GameObject gun;
	public GameObject ammoToSpawn;

	[Header("Asing by 'Ultimates' only:")]
	public GameObject ultimatePrefab;


	//public GameObject ultimateIcon;


	// Start is called before the first frame update
	void Start()
    {
		canvas = transform.parent;
        if (isGun)
        {
            animator = transform.parent.parent.Find("PIBG").GetComponent<Animator>();
        }

		equipmentUIManager = GameObject.Find("EquipmentScreen").GetComponent<EquipmentUIManager>();
		if(equipmentUIManager == null){
			throw new System.Exception("Equipment UI manager, is 'NULL' ");
		}

        // ultimateSelector = GameObject.FindGameObjectWithTag("UltimateSelector").GetComponent<UltimateSelector>();
        // gunSelector = GameObject.FindGameObjectWithTag("GunSelector").GetComponent<UltimateSelector>();
    }

    public void OpenPiOnSelection()
    {
        animator.SetBool("Open", true);
        equipmentUIManager.SetSelectedGun(gun, icon, ammoToSpawn);
    }



}
