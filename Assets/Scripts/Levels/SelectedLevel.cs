using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedLevel : MonoBehaviour
{
	private static SelectedLevel instance;
	public static SelectedLevel Instance {
		get { return instance; }
		set { instance = value;}
	}

	private void Awake()
	{
		DontDestroyOnLoad(this);
		if(instance == null){
			instance = this;
		}else if (instance != this){
			Destroy(this.gameObject);
		}
	}

	[SerializeField]
	private LevelObject level;

	[SerializeField]
	private LevelObject survivorLevel;


	public void SetSurvivorlevel()
	{
		level = survivorLevel;
	}

	public void SetLevel(LevelObject _level){
		level = _level;
	}

	public LevelObject GetSurvivorLevel(){
		return survivorLevel;
	}

	public LevelObject GetLevel(){
		return level;
	}

	public List<GameObject> GetEnemyList(){
		return level.enemyes;
	}

	public List<GameObject> GetBossList()
	{
		return level.bosses;
	}

	public List<GameObject> GetObstacleList()
	{
		return level.obstacles;
	}

	public List<GameObject> GetPowerupList()
	{
		return level.powerups;
	}

	public void OnEnemySpawned(){
		
	}

	public void OnObstacleSpawned(){
		
	}

	public bool IsBossLevel(){
		return level.bossLevel;
	}

	public void CompletteLevel(){
		level.completed = true;
	}

}
