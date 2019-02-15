using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public PiUI inventoryUI;

	[SerializeField]
	float slowAmount;
	Vector2 playerPos;
	Transform player;
	float _fixedDeltaTime;
	private void Start()
	{
		_fixedDeltaTime = Time.fixedDeltaTime;
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}


	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			Time.timeScale = slowAmount;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			playerPos  = (Vector2)Camera.main.WorldToScreenPoint(player.position);
			inventoryUI.OpenMenu(playerPos);
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			Time.timeScale = 1;
			Time.fixedDeltaTime = _fixedDeltaTime;
			inventoryUI.CloseMenu();
		}
	}
}
