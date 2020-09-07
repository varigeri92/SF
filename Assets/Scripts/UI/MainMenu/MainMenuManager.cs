using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{


    public GameObject[] panels;

    public GameObject introText;

    public UnityEngine.UI.Button playButton;

    private void Start()
    {
        if (!PresistentOptionsManager.Instance.justStarted)
        {
            closePanels();
            introText.SetActive(false);
            OpenPanel(0);

            if (!PresistentOptionsManager.Instance.survival)
            {
                playButton.onClick.Invoke();
            }
            else
            {
                PlayOpenAnim(0);
            }
        }
    }

    // Start is called before the first frame update
    public void closePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel(int _panelID)
    {
        panels[_panelID].SetActive(true);
    }

    void PlayOpenAnim(int _panelID)
    {
       panels[_panelID].GetComponent<UIPanel>().Show();
    }

    public void ClosePanel(int _panelID)
    {
        panels[_panelID].SetActive(false);
    }
}
