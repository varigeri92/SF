using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelObject : ScriptableObject
{
	[Header("Data to show on Card:")]
	public int completionStars;
	public bool available;
	public bool completed;

	[Header("Level Properties:")]
	public Sprite backgroundImage;

	public List<GameObject> enemyes;
	public List<GameObject> bosses;
	public List<GameObject> obstacles;
	public List<GameObject> powerups;

	public bool bossLevel;
	public float timeToBoss;

	public int starsToSpawn;
	public float timeBetweenStars = 15;

	public int enemiesToShoot;
	public float timeBetweenSpawns;
	public int enemyCount;


	[Header("Level Statistics:")]
	public int completedInSeconds;
	public int starsShot;
	public int powerupsCollected;
	public int powerCoresCollected;
	[Header("Additional Statistics:")]
	public int powerUpsSpawned;
	public int powerCoresSpawned;


}
