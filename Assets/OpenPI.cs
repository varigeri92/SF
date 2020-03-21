using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPI : MonoBehaviour
{

    public PiUI pi;
    public Vector2 screenCenter;


    private void Start()
    {
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }


    public void OpenPi()
    {
        pi.OpenMenu(screenCenter);
    }

    public void ClosePi()
    {
        pi.openedMenu = false;
    }
}
