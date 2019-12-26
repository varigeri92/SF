using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[CreateAssetMenu(fileName = "new PlayerState", menuName = "PlayerStateObject")]
public class PlayerStateObject : ScriptableObject
{
	public List<PlayerGun> allPlayerGuns;
	public List<int> availablePlayerGuns;
	public List<int> equipedPlayerGuns;

	public List<PlayerUltimate> allPlayerUltimates;
	public List<int> availablePlayerUltimates;
	public int equipedPlayerUltimate;


	public List<UpgradeButtonObject> perks;

	public List<SaveLevel> levelProgress;
}


[System.Serializable]
public class PerkUpgradeSave{
	public int index;
	public bool value;

	public PerkUpgradeSave(int index, bool value)
	{
		this.index = index;
		this.value = value;
	}
}

[System.Serializable]
public class PlayerGun
{
	public GameObject gunPrefab;
	public GameObject gunIcon;
	public GameObject gunGridElement;
	public GameObject gunCollectablePrefab;
	public GunObject gunObject;
	public SerializableGunObject serializableGun;
}

[System.Serializable]
public class SerializableGunObject{
	public int startingAmmo;
	public int maxAmmo;
	public float fireRate;
	public int damage;
	public int index;

	public SerializableGunObject(GunObject gunObject){
		startingAmmo = gunObject.startingAmmo;
		maxAmmo = gunObject.maxAmmo;
		fireRate = gunObject.fireRate;
		damage = gunObject.damage;
		index = gunObject.index;
	}
}

[System.Serializable]
public class PlayerUltimate
{
	public GameObject ultPrefab;
	public GameObject ultIcon;
	public GameObject ultGridElement;
	public GameObject ultCollectablePrefab;
	public Abilit_Object ultObject;

}
[System.Serializable]
public class SaveLevel{
	public int index;
	public bool completed;
	public bool available;
}


