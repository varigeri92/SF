using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunObject : ScriptableObject
{
    public GameObject gunIcon;
    public GameObject gameplayPrefab;
    public GameObject equipmentGridPrefab;
    public GameObject gunCollectable;

    public int startingAmmo;
	public int maxAmmo; 
	public float fireRate;
	public int damage;
	public int index;


}
