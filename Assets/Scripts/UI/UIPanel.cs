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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && opened && inFocus)
        {
            Debug.Log(gameObject.name);
            backEvent.Invoke();
        }
    }

    public virtual void Show()
    {
        animator.SetBool("show", true);
        opened = true;
    }

    public virtual void Hide()
    {
        animator.SetBool("show", false);
        opened = false;
        SetFocus(false);
    }

    public void SetFocus(bool inFocus)
    {
        this.inFocus = inFocus;
    }
}
