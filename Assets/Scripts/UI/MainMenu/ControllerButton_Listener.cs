using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerButton_Listener : MonoBehaviour
{
    Button button;

    public string X_ControllerbuttonName;

    public string PS_ControllerbuttonName;

    [SerializeField]
    bool detectController;

    public GameObject ControllerText;
    public GameObject pcText;

    public Image buttonIconImage;

    // Start is called before the first frame update
    void Start()
    {
        if (!detectController)
            return;

        InputManager.OnStart();
        button = GetComponent<Button>();
        if (InputManager.usingController)
        {
            pcText.SetActive(false);
            switch (InputManager.GetGamePad())
            {
                case 0:
                    ControllerText.SetActive(true);
                    buttonIconImage.sprite = PresistentOptionsManager.Instance.buttonSprites[PresistentOptionsManager.Instance.buttonName.IndexOf(X_ControllerbuttonName)];
                    break;
                case 1:
                    ControllerText.SetActive(true);
                    buttonIconImage.sprite = PresistentOptionsManager.Instance.buttonSprites[PresistentOptionsManager.Instance.buttonName.IndexOf(X_ControllerbuttonName)];
                    break;
                case 2:
                    Debug.Log("Unknown gamepad Connected");
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!detectController)
            return;
        

        if (X_ControllerbuttonName != "")
        {
            if (Input.GetButtonDown(X_ControllerbuttonName))
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
                button.onClick.Invoke();
            }
        }

        if (PS_ControllerbuttonName != "")
        {
            if (Input.GetButtonDown(PS_ControllerbuttonName))
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
                button.onClick.Invoke();
            }
        }
        
    }
}
