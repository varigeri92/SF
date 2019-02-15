using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresistentOptionsManager : MonoBehaviour {

	private PresistentOptionsManager instance;

	void Awake(){
		Debug.Log("KORREKT!");
		if(instance == null){
			instance = this;
		}else{
			Destroy(this);
		}
	}
	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
