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

	Vector2 positionOnDragbegin;
	Vector2 psitionOnDragEnd;
	CanvasGroup canvasGroup;

	//public GameObject ultimateIcon;


	// Start is called before the first frame update
	void Start()
    {
		canvas = transform.parent;

		mouseDragIcon = GameObject.Find("MouseDragIcon").GetComponent<MouseDragIcon>();

		equipmentUIManager = GameObject.Find("EquipmentScreen").GetComponent<EquipmentUIManager>();
		if(equipmentUIManager == null){
			throw new System.Exception("Equipment UI manager, is 'NULL' ");
		}

        ultimateSelector = GameObject.FindGameObjectWithTag("UltimateSelector").GetComponent<UltimateSelector>();

		canvasGroup = mouseDragIcon.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }


	public void OnPointerDown(PointerEventData eventData)
	{
		mouseDragIcon.SetData(transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite);
		canvasGroup.alpha = 1;
		positionOnDragbegin = Input.mousePosition;
		if(isGun){
			equipmentUIManager.BeginDrag(gun, icon, ammoToSpawn, isGun);
		}else{
			ultimateSelector.SetSelectionOnDragBegin(transform.Find("Icon").gameObject, ultimatePrefab, isGun);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(equipmentUIManager.isMouseoverSlot){
			mouseDragIcon.GetComponent<CanvasGroup>().alpha = 0;
		}else{
			psitionOnDragEnd = mouseDragIcon.transform.position;
			Debug.Log("Hello");
			StartCoroutine(DragCanceled());
		}

	}

	IEnumerator DragCanceled(){
		float alpha = 1;
		float speed = 1f;
		while(alpha > 0.3){
			alpha -= 5f * Time.deltaTime;
			speed -= Time.deltaTime;
			canvasGroup.alpha = alpha;
			mouseDragIcon.transform.position = Vector2.Lerp(positionOnDragbegin, psitionOnDragEnd, alpha * 0.7f * speed);
			yield return null;
		}
		equipmentUIManager.CancelDrag();
		alpha = 0;
		canvasGroup.alpha = alpha;
	}

}
