using UnityEngine;


[RequireComponent(typeof(Animator))]
public class IntroText : UIPanel
{
    [SerializeField] private Animator menuAnimator;

    [SerializeField] private GameObject[] texts;

    [SerializeField] MainMenuManager menuManager;

    bool isHidden = false; 

    private void Start()
    {
        InputManager.OnStart();

        if (InputManager.usingController)
        {
            texts[0].SetActive(false);
            switch (InputManager.GetGamePad())
            {
                case 0:
                    texts[1].SetActive(true);
                    break;
                case 1:
                    texts[2].SetActive(true);
                    break;
                case 2:
                    Debug.LogWarning("Controller not supported");
                    break;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && !isHidden)
        {
            Hide();
            menuManager.closePanels();
            menuManager.OpenPanel(0);
            isHidden = true;

        }
    }

    void ShowMenu()
    {
        //menuAnimator.SetBool("show", true);
        backEvent.Invoke();
    }

    public void DisableAnimation()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = false;
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }
}
