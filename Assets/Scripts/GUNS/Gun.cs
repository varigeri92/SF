using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

	public int ammo;
	public GameObject icon;
    OutAmmoText outOfAmmoText;
	AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
        outOfAmmoText = GameObject.FindGameObjectWithTag("OutOfAmmoText").GetComponent<OutAmmoText>();

	}

	public virtual void Shooting(bool isPlayer)
	{
        if (ammo != -999 && ammo <= 0)
        {
            outOfAmmoText.Show();
        }
	}

	public virtual void Playsound (){

		// source.Play();

	}

    public virtual void StopShooting()
    {

    }
}
