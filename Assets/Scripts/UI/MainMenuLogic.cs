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


	public AudioMixer _mixer;
    public AudioSource sfxSource;
    public AudioSource musicSource;

    private void Start()
    {
        float volume  = 0f;
        _mixer.GetFloat("masterVol", out volume);
        masterSlider.value = volume;
        SetmasterVolume(volume);

        _mixer.GetFloat("musicVol", out volume);
        musicSlider.value = volume;
        SetMusicVolume(volume);

        _mixer.GetFloat("sfxVol", out volume);
        fxSlider.value = volume;
        SetFXVolume(volume);
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
}
