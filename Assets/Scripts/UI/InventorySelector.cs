using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelector : MonoBehaviour {

	public List<GameObject> items = new List<GameObject>();
	public List<GameObject> icons = new List<GameObject>();

	
	Player player;
	// Use this for initialization
	void Start()
	{
		//player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		Player.OnPlayerLoaded += OnPlayerLoaded;
	}
	
	
	// Update is called once per frame
	void Update () {
		if(player == null){
			//player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}
	}

	public void SelectWeapon(int index){
		if(index < items.Count){
			player.ChangeGun(items[index]);
		}else{
			Debug.Log("INDEX: " + index.ToString());
		}
	}

	private void OnDestroy()
	{
		Player.OnPlayerLoaded -= OnPlayerLoaded;
	}

	void OnPlayerLoaded(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		items = new List<GameObject>(player.playerObject.inventoryGuns);
		icons = new List<GameObject>(player.playerObject.inventoryIcons);

		foreach(GameObject icon in icons){
			icon.GetComponent<InventoryGun>().SetAmmo(icon.GetComponent<InventoryGun>().gunObject.startingAmmo);
		}

		player.LoadSavedInventory();
	}
}
