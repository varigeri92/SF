using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Gamepad
{
    xone,   //0
    PS4,    //1
    Other,  //2
}

enum ButtonState
{
    Released,
    Pressed,
    Held,
    none
}

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
    static Gamepad ConnectedGamepad;

    static Dictionary<string, bool> pressedButtons = new Dictionary<string, bool>();

    public static void OnStart () {
        usingController = DetectController();
	}


    static ButtonState AxesToButton(string axesName)
    {
        if (!pressedButtons.ContainsKey(axesName) )
        {
            pressedButtons.Add(axesName, false);
        }

        if (Input.GetAxisRaw(axesName) == 0 && pressedButtons[axesName])
        {
            Debug.Log(axesName + " Released!");
            pressedButtons[axesName] = false;
            return ButtonState.Released;
        }
        else if (Input.GetAxisRaw(axesName) > 0 && !pressedButtons[axesName])
        {
            Debug.Log(axesName + " Presed!");
            pressedButtons[axesName] = true;
            return ButtonState.Pressed;
        }
        else if (Input.GetAxisRaw(axesName) > 0 && pressedButtons[axesName])
        {
            Debug.Log(axesName + " Held down!");
            return ButtonState.Held;
        }
        else return ButtonState.Released;
    } 

    public static void OnUpdate () {

        if (usingController)
        {
            Debug.Log("Using Controller");

            movement = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            
            if (ConnectedGamepad == Gamepad.xone)
            {
                direction = new Vector2(Input.GetAxis("XO_RS_HOR"),  Input.GetAxis("XO_RS_VER"));
                if (AxesToButton("XONE_RT") == ButtonState.Held)
                {
                    if (OnShootButtonPresed != null)
                    {
                        OnShootButtonPresed();
                    }
                }
                if (AxesToButton("XONE_RT") == ButtonState.Released)
                {
                    if (OnShootButtonreleased != null)
                    {
                        OnShootButtonreleased();
                    }
                }

                // BOOST:
                if (AxesToButton("XONE_LT") == ButtonState.Held)
                {
                    if (OnBoostButtonPressed != null)
                    {
                        OnBoostButtonPressed();
                    }
                }

                if (AxesToButton("XONE_LT") == ButtonState.Released)
                {
                    if (OnBoostButtonReleased != null)
                    {
                        OnBoostButtonReleased();
                    }
                }

                //INVENTORY:
                if (Input.GetButtonDown("Inventory"))
                {

                    if (OnInventoryButtonPressed != null)
                    {
                        OnInventoryButtonPressed();
                    }
                }
                if (Input.GetButtonUp("Inventory"))
                {
                    if (OnInventoryButtonReleased != null)
                    {
                        OnInventoryButtonReleased();
                    }
                }

                //ULTIMATE:

                if (Input.GetButtonDown("Fire2"))
                {
                    if (OnUltimateButtonPressed != null)
                    {
                        OnUltimateButtonPressed();
                    }
                }

            }
            else
            {
                direction = new Vector2(Input.GetAxis("Right_Stick_X"), Input.GetAxis("Right_Stick_Y"));
                //FIRE:
                if (Input.GetButton("Fire1")){
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
        }
        else if(!usingController || ConnectedGamepad == Gamepad.Other)
        {
            Debug.Log("DONT!!! Using Controller");
            if (ConnectedGamepad == Gamepad.Other)
            {
                usingController = false;
            }
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
                switch (Input.GetJoystickNames()[0])
                {
                    case "Xbox Bluetooth Gamepad":
                        ConnectedGamepad = Gamepad.xone;
                        Debug.Log("XboxController Connected");
                        break;
                    default:
                        ConnectedGamepad = Gamepad.Other;
                        Debug.Log("The controller you using is unknown! this can lead to Issues in the controll!");
                        return false;
                        // break;
                }

#if UNITY_EDITOR
                foreach (string joystickName in Input.GetJoystickNames())
                {
                    Debug.Log("There is a Connected Jojstick:   " + joystickName);
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

    public static int GetGamePad()
    {
        return ConnectedGamepad.GetHashCode();
    }
}
