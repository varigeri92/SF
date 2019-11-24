using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	public List<GameObject> obstacles = new List<GameObject>();
	public List<Transform> spawnPoints = new List<Transform>();
	public List<Transform> targets = new List<Transform>();
	public List<GameObject> powerUps = new List<GameObject>();

	public int powerupSpawnRate;
	int currentPuwerupSpawnState = 0;

	public float forceMin = 10;
	public float forceMax = 15;

	public float spawnRate = 5;

	public int count = 4;
	int random;

	float timer = 0;

	// Use this for initialization
	void Start () {
		obstacles = new List<GameObject>(SelectedLevel.Instance.GetObstacleList());
		powerUps = new List<GameObject>(SelectedLevel.Instance.GetPowerupList());
	}
	
	// Update is called once per frame
	void Update () {
		random = Random.Range(1,5);
		timer += spawnRate * Time.deltaTime;
		if (timer >= 1){
			if (random == 2 || random == 1){
				SpawnObs();
				currentPuwerupSpawnState++;
				if (currentPuwerupSpawnState >= powerupSpawnRate){
					SpawnPowerups();
					currentPuwerupSpawnState = 0;
				}
			}

			SpawnObs();


			timer = 0;
		}
	}

	private void SpawnObs(){
		if(obstacles.Count > 0)
		{
			int rnd = Random.Range(0,spawnPoints.Count);
			int rndObstacle = Random.Range(0, obstacles.Count);
			GameObject go = Instantiate(obstacles[rndObstacle],spawnPoints[rnd].position,Quaternion.identity);
			// Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
			int rnd2 = Random.Range(0, targets.Count);

			Vector3 dir =  targets[rnd2].position - go.transform.position;
			float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
			go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

			// float frnd = Random.Range(forceMin, forceMax);
			// rb.AddForce((targets[rnd2].position - spawnPoints[rnd].position) * frnd, ForceMode2D.Impulse);
		}
	}

	private void SpawnPowerups()
	{
		int rnd = Random.Range(0, spawnPoints.Count);
		int rndPowerup = Random.Range(0,powerUps.Count);
		GameObject go = Instantiate(powerUps[rndPowerup], spawnPoints[rnd].position, Quaternion.Euler(Vector3.down));
		Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
		int rnd2 = Random.Range(0, targets.Count);
		float frnd = Random.Range(forceMin / 3, forceMax / 3);
		rb.AddForce((targets[rnd2].position - spawnPoints[rnd].position) * frnd, ForceMode2D.Impulse);
	}
}
