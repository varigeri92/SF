using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IntroText : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Animator menuAnimator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            HideText();
        }
    }

    void HideText()
    {
        animator.SetBool("show", false);
    }

    void ShowMenu()
    {
        menuAnimator.SetBool("show", true);
    }
}
