using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EnemySpawner : MonoBehaviour {

    public bool dontSpawn;
    public int spawnOnly;

	public List<GameObject> enemyes = new List<GameObject>();
	public List<Transform> points = new List<Transform>();
	public List<BasicEnemy> spawnedEnemyes = new List<BasicEnemy>();

	[SerializeField]
	private int maxEnemies = 10;

    public int playerLevel;

	public int count;
	public float rate;
	public float time = 0;

	bool playerAlive = true;

	int lastrandomindex;

	int spawnedEnemies = 0;
	int enemiesToSpawn;


	void Start()
	{
		enemiesToSpawn = SelectedLevel.Instance.GetLevel().enemiesToShoot;

		for (int i = 0; i < transform.childCount; i++) {
			Transform t = transform.GetChild(i);
			points.Add(t);
		}

		enemyes = new List<GameObject>(SelectedLevel.Instance.GetEnemyList());

		BasicEnemy.onEnemyDead += RemoveEnemyFromList;
        Player.OnPlayerDeath += PlayerDied;
        Player.OnLevelUp += LevelUp;
	}
    
    void PlayerDied()
    {
        playerAlive = false;
    }

    private void OnDestroy()
    {
        Player.OnPlayerDeath -= PlayerDied;
        Player.OnLevelUp -= LevelUp;
    }

    void RemoveEnemyFromList(BasicEnemy enemy){
		spawnedEnemyes.Remove(enemy);
	}
	

	void Update () {
		time += Time.deltaTime * rate;
		if(time >= 1){
			for (int i = 0; i < count; i++) {
				if (playerAlive)
                {
                    if(!dontSpawn)
    					Spawn();
                }
			}
			time = 0;
		}
	}

	void Spawn(){

		if(spawnedEnemyes.Count >= maxEnemies){
			Debug.Log("MAX ENEMY COUNT REACHED!");
			return;
		}

		if(spawnedEnemies == enemiesToSpawn){
			Debug.Log("All the enemyes are spawned!!");
			return;
		}

        int enemyToSpawn;
        if (spawnOnly < 0 || spawnOnly >= enemyes.Count)
        {
            enemyToSpawn = (int)Random.Range(0, enemyes.Count);
        }
        else
        {
            enemyToSpawn = spawnOnly;
        }

		int rand = Random.Range(0,points.Count);
		if(rand == lastrandomindex){
			if(rand == 0){
				rand++;
			}else if (rand == points.Count -1){
				rand--;
			}else{
				rand++;
			}
		}
		lastrandomindex = rand;
		spawnedEnemies++;
		GameObject go = Instantiate(enemyes[enemyToSpawn], points[rand].position, points[rand].transform.rotation);
		spawnedEnemyes.Add(go.GetComponent<BasicEnemy>());
	}

	public void SpawnBoss(GameObject boss){
		int rand = Random.Range(0, points.Count);
		if (rand == lastrandomindex) {
			if (rand == 0) {
				rand++;
			} else if (rand == points.Count - 1) {
				rand--;
			} else {
				rand++;
			}
		}
		GameObject go = Instantiate(boss, points[rand].position, points[rand].transform.rotation);
		spawnedEnemyes.Add(go.GetComponent<BasicEnemy>());
	}

    void LevelUp(int level)
    {
        playerLevel = level;
    }
}
