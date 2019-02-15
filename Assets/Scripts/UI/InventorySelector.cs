using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelector : MonoBehaviour {

	public List<GameObject> items = new List<GameObject>();
	public List<GameObject> icons = new List<GameObject>();

	
	Player player;
	// Use this for initialization
	void Start () {
		player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectWeapon(int index){
		if(index < items.Count){
			player.ChangeGun(items[index]);
		}else{
			Debug.Log("INDEX: " + index.ToString());
		}
	}
}
