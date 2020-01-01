using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopUISound : MonoBehaviour
{
	[SerializeField]
	AudioClip hoverSound;

	[SerializeField]
	AudioClip upgradeSound;

	AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
	}

	public void OnHoverPlay(){
		source.clip = hoverSound;
		source.Play();
	}

	public void OnUpgradePlay(){
		source.clip = upgradeSound;
		source.Play();
	}
}
