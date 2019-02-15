using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
	Shooter,
	Kamikaze,
	Boss
}
[CreateAssetMenu(fileName = "New Enemy",  menuName = "Enemy")]
public class EnemyObject : ScriptableObject{

	public GameObject exposionFX;

	public int helath;

	public float mooveSpeed;

	public float rotationSpeed;

	public EnemyType type;

	[Range(5,10)]
	public float fireRate;




}
