using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager{


    #region Events:
    //Call this event on hold to
    public delegate void ShootButtonPresed();
    public static event ShootButtonPresed OnShootButtonPresed;

    public delegate void ShootButtonReleased();
    public static event ShootButtonReleased OnShootButtonreleased;

    public delegate void UltimateButtonPressed();
    public static event UltimateButtonPressed OnUltimateButtonPressed;

    public delegate void BoostButtonPressed();
    public static event BoostButtonPressed OnBoostButtonPressed;

    public delegate void BoostButtonReleased();
    public static event BoostButtonReleased OnBoostButtonReleased;

    public delegate void InventoryButtonPressed();
    public static event InventoryButtonPressed OnInventoryButtonPressed;

    public delegate void InventoryButtonReleased();
    public static event InventoryButtonReleased OnInventoryButtonReleased;
    #endregion


    static Vector2 direction;
    static Vector2 movement;

    static Vector2 mousePosition;


    public static bool usingController = false;


    public static void OnStart () {
        usingController = DetectController();

	}


    public static void OnUpdate () {

        if (usingController)
        {
		    direction = new Vector2(Input.GetAxis("Right_Stick_X"), Input.GetAxis("Right_Stick_Y"));
			movement = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

			//FIRE:
			if(Input.GetButton("Fire1")){
				Debug.Log("FIRE PRESSED!!! BOI!!");
				if (OnShootButtonPresed != null) {
					OnShootButtonPresed();
				}
			}
			if(Input.GetButtonUp("Fire1")){
				Debug.Log("FIRE RELEASED!!! BOI!!");
				if (OnShootButtonreleased != null) {
					OnShootButtonreleased();
				}
			}

			// BOOST:
			if (Input.GetButtonDown("Jump")) {
				Debug.Log("HELLO");
				if (OnBoostButtonPressed != null) {
					OnBoostButtonPressed();
				}
			}

			if (Input.GetButtonUp("Jump")) {
				Debug.Log("JUMP RELEASE!");
				if (OnBoostButtonReleased != null) {
					OnBoostButtonReleased();
				}
			}

			// INVENTORY:
			if (Input.GetButtonDown("Inventory")) {
				
				if (OnInventoryButtonPressed != null) {
					OnInventoryButtonPressed();
				}
			}
			if (Input.GetButtonUp("Inventory")) {
				Debug.Log("INVENTORY RELEASED!!! BOI!!");
				if (OnInventoryButtonReleased != null) {
					OnInventoryButtonReleased();
				}
			}

			//ULTIMATE:

			if (Input.GetButtonDown("Fire2")) {
				if (OnUltimateButtonPressed != null) {
					OnUltimateButtonPressed();
				}
			}
        }
        else
        {
            // horizontal = Input.GetAxis("Horizontal");
            // vertical = Input.GetAxis("Vertical");
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            mousePosition = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                if (OnShootButtonPresed != null)
                {
                    OnShootButtonPresed();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (OnShootButtonreleased != null)
                {
                    OnShootButtonreleased();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (OnUltimateButtonPressed != null)
                {
                    OnUltimateButtonPressed();
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (OnInventoryButtonPressed != null)
                {
                    OnInventoryButtonPressed();
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (OnInventoryButtonReleased != null)
                {
                    OnInventoryButtonReleased();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnBoostButtonPressed != null)
                {
                    OnBoostButtonPressed();
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (OnBoostButtonReleased != null)
                {
                    OnBoostButtonReleased();
                }
            }

        }
	}


    public static Vector2 GetMovement()
	{
		return movement;
	}
    public static Vector2 GetDirection()
	{
		return direction;
	}

    public static Vector2 GetMousePosition()
    {
        return mousePosition;
    }

    static void ForceUseController()
    {
        usingController = true;
    }

    static bool DetectController()
    {
        if (Input.mousePresent)
        {
            Debug.Log(" There is a mouse! ");
            if (Input.GetJoystickNames().Length > 0 )
            {
                Debug.Log("Following josticks are connected: ");
#if UNITY_EDITOR
                foreach (string joystickName in Input.GetJoystickNames())
                {
                    Debug.Log(joystickName);
                }
#endif
                return true;
            }else
            {
                Debug.Log("No Controller connected! ");
                return false;
            }
        }
        else
        {
            Debug.Log(" There is no mouse! ");
            return true;
        }
    }
}
