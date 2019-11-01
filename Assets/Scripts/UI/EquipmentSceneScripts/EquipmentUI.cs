using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	public bool isDragged = false;
	public Transform canvas;
	public MouseDragIcon mouseDragIcon;

	// Start is called before the first frame update
	void Start()
    {
		canvas = GameObject.Find("Canvas").transform;

		mouseDragIcon = GameObject.Find("MouseDragIcon").GetComponent<MouseDragIcon>();
    }

    // Update is called once per frame
    void Update()
    {

    }


	public void OnPointerDown(PointerEventData eventData)
	{
		mouseDragIcon.SetData(transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite);
		mouseDragIcon.GetComponent<CanvasGroup>().alpha = 1;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		mouseDragIcon.GetComponent<CanvasGroup>().alpha = 0;
	}


}
