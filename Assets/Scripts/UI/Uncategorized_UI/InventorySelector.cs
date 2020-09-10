using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelector : MonoBehaviour
{
	Player player;


	void Start()
	{
		Player.OnPlayerLoaded += OnPlayerLoaded;
	}

    void OnDestroy()
    {
        Player.OnPlayerLoaded -= OnPlayerLoaded;
    }

    void Update () {

	}

	public void SelectWeapon(int index){
		if(index < Global.Instance.PlayerData.equipedGuns.Count){
			player.ChangeGun(Global.Instance.PlayerData.equipedGuns[index].gameplayPrefab);
		}else{
			Debug.Log("INDEX: " + index.ToString());
		}
	}



	void OnPlayerLoaded(Player player){

        this.player = player;
        foreach (GunObject gunObject in Global.Instance.PlayerData.equipedGuns)
        {
            InventoryGun iGun = gunObject.gunIcon.GetComponent<InventoryGun>();
            iGun.SetStartingAmmo();
        }

		player.LoadSavedInventory();
    }

}
