using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HowerText : MonoBehaviour
{
	[SerializeField]
	TMP_Text description;

	[SerializeField]
	TMP_Text tittle;

    CanvasGroup cg;

    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        PerkButton.OnPointerEntered += EnableHowerText;
        PerkButton.OnPointerExited  += DisableHowerText;
    }

    private void OnDestroy()
    {
        PerkButton.OnPointerEntered -= EnableHowerText;
        PerkButton.OnPointerExited  -= DisableHowerText;
    }


    // Update is called once per frame
    void Update()
    {
		transform.position = Input.mousePosition;
    }
    void EnableHowerText(string description, string tittle)
    {
        cg.alpha = 1;
        this.description.text = description;
        this.tittle.text = tittle;
    }

    void DisableHowerText()
    {
        cg.alpha = 0;
    }
}
