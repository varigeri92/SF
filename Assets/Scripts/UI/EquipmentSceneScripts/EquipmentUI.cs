using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	public bool isGun = true;
	[HideInInspector]
	public bool isDragged = false;
	[HideInInspector]
	public Transform canvas;
	[HideInInspector]
	public MouseDragIcon mouseDragIcon;
	[HideInInspector]
	public EquipmentUIManager equipmentUIManager;

	public UltimateSelector ultimateSelector;

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
		canvas = GameObject.Find("Canvas").transform;

		mouseDragIcon = GameObject.Find("MouseDragIcon").GetComponent<MouseDragIcon>();

		equipmentUIManager = canvas.GetComponent<EquipmentUIManager>();

		ultimateSelector = canvas.GetComponentInChildren<UltimateSelector>();
    }

    // Update is called once per frame
    void Update()
    {

    }


	public void OnPointerDown(PointerEventData eventData)
	{
		mouseDragIcon.SetData(transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite);
		mouseDragIcon.GetComponent<CanvasGroup>().alpha = 1;
		equipmentUIManager.BeginDrag(gun, icon, ammoToSpawn, isGun);
		ultimateSelector.SetSelectionOnDragBegin(icon, ultimatePrefab, isGun);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		mouseDragIcon.GetComponent<CanvasGroup>().alpha = 0;
	}


}
