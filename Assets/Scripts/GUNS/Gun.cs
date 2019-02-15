using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

	public int ammo;
	public GameObject icon;
	AudioSource source;

	private void OnEnable()
	{
		source = GetComponent<AudioSource>();
	}

	public virtual void Shooting(bool isPlayer)
	{
		
	}

	public virtual void Playsound (){

		source.Play();

	}
}
