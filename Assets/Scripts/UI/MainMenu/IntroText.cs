using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IntroText : UIPanel
{
    [SerializeField] private Animator menuAnimator;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            Hide();
        }
    }

    void ShowMenu()
    {
        menuAnimator.SetBool("show", true);
    }
}
