using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIPanel : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    public virtual void Show()
    {
        animator.SetBool("show", true);
    }

    public virtual void Hide()
    {
        animator.SetBool("show", false);
    }
}
