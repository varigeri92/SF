using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunObject : ScriptableObject
{
	public int maxAmmo; 
	public float fireRate;
	public int damage;

}
