﻿using System.Collections;
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
		if(instance == null){
			instance = this;
		}else if (instance != this){
			Destroy(gameObject);
		}

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
