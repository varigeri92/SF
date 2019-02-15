using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	Vector2 direction;
	Vector2 movement;

	void Start () {
		
	}
	

	void Update () {

		direction = new Vector2(Input.GetAxis("Right_Stick_X"), Input.GetAxis("Right_Stick_Y"));
		// movement = new Vector2(Input.GetAxis(""),Input.GetAxis(""));

	}
	public Vector2 GetMovement()
	{
		return movement;
	}
	public Vector2 GetDirection()
	{
		return direction;
	}
}
