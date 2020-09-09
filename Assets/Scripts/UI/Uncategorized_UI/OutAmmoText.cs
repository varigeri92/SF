using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutAmmoText : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] float fadeSpeed;

    IEnumerator _fadeText;
    
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        _fadeText = FadeText();
    }
    public void Show()
    {
        StopCoroutine(_fadeText);
        _fadeText = FadeText();
        canvasGroup.alpha = 1;
        StartCoroutine(_fadeText);
    }

    IEnumerator FadeText ()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return 0;
        }
    }
}
