using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGun : MonoBehaviour {

	[SerializeField]
	GameObject prefab;
	[SerializeField]
	GameObject icon;
	Counter counter = new Counter();
	[SerializeField]
	float lifetime;


	// Use this for initialization
	void Start () {
		//StartCoroutine(counter.Countdow(lifetime,this.gameObject));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player"){
			other.GetComponent<Player>().PickupGun(prefab,icon);
			Destroy(gameObject);
		}
	}
}
