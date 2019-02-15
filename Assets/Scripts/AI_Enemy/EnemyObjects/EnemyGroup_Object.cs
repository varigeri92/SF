using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Group_Type{
 	Obstacle,
	ComplexObstacle,
	SimpleEnemy,
	ComplexEnemy
}
public enum Group_Shape{
	Circle,
	Abstract,
	Cross
}
[CreateAssetMenu(fileName = "New Group",  menuName = "Enemy Group")]
public class EnemyGroup_Object : ScriptableObject{

	public Group_Type type;
	public Group_Shape shape;

	public List <GameObject> toSpawn = new List<GameObject>();

	
}
