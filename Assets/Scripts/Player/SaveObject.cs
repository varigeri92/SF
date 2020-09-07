using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SaveObject", menuName = "SaveObject")]
public class SaveObject : ScriptableObject
{
	public int[] equipedGunIndexes;
	public int[] unlockedGunIndexes;
	public int[] unlockedUltimateIndexes;
	public int lastCompletedLevel;

	public int selectedUltimate;

	public int pmaxHealth;
	public float pspeed;
	public float pboostFillSpeed;
	public float pboostDecSpeed;
	public int ppowerCores;
	public int plevel;

	public SerializableGunObject[] allGunObject;
    public SerializableUltimateObject[] allUltobjects;
	public PerkUpgradeSave[] perks;

}
