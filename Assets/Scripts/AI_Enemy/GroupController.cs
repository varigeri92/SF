using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour {

	public EnemyGroup_Object group;
	public float radius;

	public bool isRotating;
	public float rotationSpeed;
	void Start(){
		SpawnObjects();
	}
	void SpawnObjects(){
		float deg = 360 / group.toSpawn.Count;
		int index = 0;
		
		
		
		switch(group.shape){
			case  Group_Shape.Circle :
				foreach(GameObject go in group.toSpawn){
					Vector2 point = GetUnitOnCircle(deg * index,radius);
					GameObject spawnedObject =  Instantiate(go, new Vector3(point.x,point.y,radius), Quaternion.identity);
					spawnedObject.transform.SetParent(transform);
					index ++;
					
				}
			return;
		}
		
		

	}

	Vector2 GetUnitOnCircle(float angleDegrees, float radius) {
 
    // initialize calculation variables
    float _x = 0;
    float _y = 0;
    float angleRadians = 0;
    Vector2 _returnVector;
    // convert degrees to radians
    angleRadians = angleDegrees * Mathf.PI / 180.0f;
    // get the 2D dimensional coordinates
    _x = radius * Mathf.Cos(angleRadians);
    _y = radius * Mathf.Sin(angleRadians);
    // derive the 2D vector
    _returnVector = new Vector2(_x, _y) + (Vector2)transform.position;
	// return the vector info
    return _returnVector;
 }

	// Update is called once per frame
	void Update () {
		//if(isRotating){
		transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
		// }
	}
}
