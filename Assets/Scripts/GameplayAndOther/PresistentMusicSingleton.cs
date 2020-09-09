using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresistentMusicSingleton : MonoBehaviour
{

	static PresistentMusicSingleton instance;

	public static PresistentMusicSingleton Instance {
		get {
			return instance;
		}

		set {
			instance = value;
		}
	}
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if(Instance == null){
			Instance = this;
		}else if (Instance != this){
			Destroy(gameObject);
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.Stop();
		audioSource.Play();


	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
