using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour {

	public GameObject InventoryPanel;
	public Player player;

	public Transform marker;

	public Transform[] slotPositions;
	int selected = 0;
	[SerializeField]
	float speed;

	IEnumerator _SelectAnimation;

	bool keyPressed = false;


	public void OnCollectablepickedUp(){
		
	}

	void SelectElement(float value){
		if (value > 0 ) {
			selected++;
			if (selected > slotPositions.Length - 1) {
				selected = 0;
			}
			if(slotPositions[selected].GetComponent<SelectorBehaviour>().gun != null){
				player.ChangeGun(slotPositions[selected].GetComponent<SelectorBehaviour>().gun);
			}
			// marker.transform.position = slotPositions[selected].position;
			if(_SelectAnimation != null){
				StopCoroutine(_SelectAnimation);
			}
			_SelectAnimation = MoveMarker(slotPositions[selected]);
			StartCoroutine(_SelectAnimation);
		}
		else if(value < 0)
		{
			selected--;
			if (selected < 0) {
				selected = slotPositions.Length -1;
			}
			if (slotPositions[selected].GetComponent<SelectorBehaviour>().gun != null) {
				player.ChangeGun(slotPositions[selected].GetComponent<SelectorBehaviour>().gun);
			}
			// marker.transform.position = slotPositions[selected].position;
			if (_SelectAnimation != null) {
				StopCoroutine(_SelectAnimation);
			}
			_SelectAnimation = MoveMarker(slotPositions[selected]);
			StartCoroutine(_SelectAnimation);
		}
	}

	void TriggerChange(){
		
	}

	public void ChangeWeapon(GameObject gun){
		player.ChangeGun(gun);
	}

	// Use this for initialization
	void Start()
	{
		// slotPositions = InventoryPanel.GetComponentsInChildren<Transform>();
		// marker.transform.localPosition = new Vector2(25,-27);
			//slotPositions[selected].position;

	}
	// Update is called once per frame
	void Update () {

		float keyValue = Input.GetAxisRaw("D_Pad Y");

		if (!keyPressed){
			SelectElement(keyValue);
			keyPressed = true;
		}

		if (keyValue == 0) {
			keyPressed = false;
		}
	}

	IEnumerator MoveMarker(Transform target){
		float distance = 0;
		float duration = speed;
		Vector2 markerPos = marker.transform.position;
		while(distance < 1){
			distance += Time.deltaTime / duration;
			marker.transform.position = Vector2.Lerp(markerPos,target.position,distance);
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}
}
