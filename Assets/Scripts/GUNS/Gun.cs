using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GunObject gunObject;
	public int ammo;
	public GameObject icon;
    OutAmmoText outOfAmmoText;
	AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
        outOfAmmoText = GameObject.FindGameObjectWithTag("OutOfAmmoText").GetComponent<OutAmmoText>();

	}

	public void AddAmmo(int _ammo){
        Debug.Log("Adding ammo to " + gunObject.name + " " + ammo.ToString());
		int precalculatedAmmo = _ammo + ammo;
		if(precalculatedAmmo >= gunObject.maxAmmo){
			ammo = gunObject.maxAmmo;
		}else{
			ammo = precalculatedAmmo;
		}
	}

	public virtual void Shooting(bool isPlayer)
	{
        if (ammo != -999 && ammo <= 0)
        {
            outOfAmmoText.Show();
        }
	}

	public virtual void Playsound (){
		if(source != null){
			source.Play();
		}else{
			Debug.LogWarning("No Audio source on the game object: " + gameObject.name);
		}

	}

    public virtual void StopShooting()
    {

    }
}
