using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundEffectManager : MonoBehaviour
{

	public AudioClip acceptSound;
	public AudioClip declineSound;
	public AudioClip deniedSound;

	public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
		source = GetComponent<AudioSource>();
    }


	public void PlayAccept(){
		source.clip = acceptSound;
		source.Play();
	}

	public void PlayDecline()
	{
		source.clip = declineSound;
		source.Play();
	}

	public void PlayDenied()
	{
		source.clip = deniedSound;
		source.Play();
	}
}
