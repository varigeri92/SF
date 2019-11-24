using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Obstacle")]
public class ObstacleObject : ScriptableObject
{
	public float moveSpeed;
	public float lifetime;
	public int dmg;
	public int health;
}
