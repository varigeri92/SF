using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Abilit_Object : ScriptableObject
{
	public int damage;
	public int charges;
	public float duration;
	public GameObject effect;
	public GameObject icon;
}
