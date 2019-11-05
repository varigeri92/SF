using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
	Shooter,
	Kamikaze,
	Boss,
    Swarm
}
[CreateAssetMenu(fileName = "New Enemy",  menuName = "Enemy")]
public class EnemyObject : ScriptableObject{

	public GameObject exposionFX;
	public GameObject powerCore;

	public float shootingDistance;

	public int health;

	public float mooveSpeed;

	public float rotationSpeed;

	public EnemyType type;

	[Range(5,10)]
	public float fireRate;

    public int xp;

	public float chancetoSpawn;
	public int coresToSpawn;


}
