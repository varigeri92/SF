using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MainMenuLogic : MonoBehaviour {

	public bool controllerInput;
	public float fadeTime;
    public Slider masterSlider;
    public Slider fxSlider;
    public Slider musicSlider;


	[SerializeField]
	GameObject FirstButton;

	[SerializeField]
	GameObject ExitNo;

	[SerializeField]
	GameObject settings_1;


    AudioSource _source;

	CanvasGroup currentOpenedPanel;
	public AudioMixer _mixer;
	void Start () {
		EventSystem.current.firstSelectedGameObject = FirstButton;
		_source = GetComponentInChildren<AudioSource>();
		currentOpenedPanel = transform.GetChild(0).GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
		SceneManager.LoadScene(1);
	}

	public void StartSurvivorMode(){
		SelectedLevel.Instance.SetSurvivorlevel();
		StartGame();
	}

	public void ToLevels(){
		SceneManager.LoadScene(2);
	}

	public void ExitGame(){
		Application.Quit();
	}

	public void OpenExitPanel(CanvasGroup cg){
		EventSystem.current.SetSelectedGameObject(ExitNo);
		StartCoroutine(FadePanelsOut(currentOpenedPanel));
		StartCoroutine(FadePanelsIn(cg));
	}

	public void CloseExitPanel(CanvasGroup cg)
	{
		EventSystem.current.SetSelectedGameObject(FirstButton);
		StartCoroutine(FadePanelsOut(currentOpenedPanel));
		StartCoroutine(FadePanelsIn(cg));
	}
    public void OpenControlls(CanvasGroup cg)
    {
		EventSystem.current.SetSelectedGameObject(settings_1);
        StartCoroutine(FadePanelsOut(currentOpenedPanel));
        StartCoroutine(FadePanelsIn(cg));
    }
    public void CloseControlls(CanvasGroup cg)
    {
		EventSystem.current.SetSelectedGameObject(FirstButton);
        StartCoroutine(FadePanelsOut(currentOpenedPanel));
        StartCoroutine(FadePanelsIn(cg));
    }

    public void SetmasterVolume(float vol){
		_mixer.SetFloat("masterVol", masterSlider.value);
	}

	public void SetMusicVolume(float vol){
		_mixer.SetFloat("musicVol", musicSlider.value);
	}

	public void SetFXVolume(float vol){
		_mixer.SetFloat("sfxVol", fxSlider.value);
	}

	public void saveOptions(){

	}

	public void RevertOtions(){
		
	}

	IEnumerator FadePanelsIn(CanvasGroup cg){
		cg.gameObject.SetActive(true);
		currentOpenedPanel = cg;

		while (cg.alpha < 1)
		{
			cg.alpha += fadeTime;
			yield return null;
		}
		if(cg.alpha >= 1){
			cg.alpha = 1;
		}
		yield break;
	}

	IEnumerator FadePanelsOut(CanvasGroup cg){
		
		while (cg.alpha > 0)
		{
			cg.alpha -= fadeTime;
			yield return null;
		}
		if(cg.alpha <= 0){
			cg.alpha = 0;
			cg.gameObject.SetActive(false);
		}
		yield break;
	}
}
