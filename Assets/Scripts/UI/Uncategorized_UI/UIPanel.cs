using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class UIPanel : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private bool opened = false;
    [SerializeField]
    private bool inFocus = false;

    public UnityEvent backEvent;

    MainMenuManager menuManager;
    public int panelID;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        menuManager = GameObject.FindGameObjectWithTag("Menu_Canvas").GetComponent<MainMenuManager>();
    }



    public virtual void Show()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetBool("show", true);
        opened = true;
    }

    public virtual void Hide()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetBool("show", false);
        opened = false;
        SetFocus(false);
    }

    public void SetFocus(bool inFocus)
    {
        this.inFocus = inFocus;
    }

    public void PanelClosed()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        menuManager.ClosePanel(panelID);
    }
}
