using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[HideInInspector]
	public int health;
	[HideInInspector]
	public Transform target;
	[HideInInspector]
	public Rigidbody2D rb;

	public bool playerAlive = true;
	[HideInInspector]
	public bool left = true;
	[HideInInspector]
	public float distance;


	public virtual void Die(){}

	public virtual void TakeDmg(int dmg){}

}
