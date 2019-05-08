using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenucontroller : MonoBehaviour
{
    [SerializeField]
    GameObject MainButtons;
    [SerializeField]
    GameObject ControllsPanel;
    [SerializeField]
    GameObject ExitPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCloseControlls(bool open)
    {
        if (ControllsPanel.activeSelf)
        {
            ControllsPanel.SetActive(open);
            MainButtons.SetActive(!open);
        }
        else
        {
            ControllsPanel.SetActive(open);
            MainButtons.SetActive(!open);
        }
    }

    public void OpenCloseExitPanel(bool open)
    {
        if (ControllsPanel.activeSelf)
        {
            ExitPanel.SetActive(open);
            MainButtons.SetActive(!open);
        }
        else
        {
            ExitPanel.SetActive(open);
            MainButtons.SetActive(!open);
        }
    }

    

}
