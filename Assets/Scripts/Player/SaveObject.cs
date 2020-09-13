using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SaveObject", menuName = "SaveObject")]
public class SaveObject : ScriptableObject
{
	
    public List<GunObject> guns = new List<GunObject>();
    public List<Perk> perks = new List<Perk>();

}
