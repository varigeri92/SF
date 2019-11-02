using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerObject : ScriptableObject{

	public int maxHealth;
	public float speed;
	public float boostFillSpeed;
	public float boostDecSpeed;
	public int powerCores;
	public int level;

	public List<GameObject> inventoryGuns;
	public List<GameObject> inventoryIcons;

	public List<GameObject> availableGuns;

}
