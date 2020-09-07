using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{

	public delegate void LvlCompleted();
	public static event LvlCompleted OnLevelCompleted;


	public EnemySpawner enemySpawner;
	public GameObject playerPrefab;

	[Header("Player References")]

	public GameSystemManager gameSystemManager;
	//-----------------------------//

	public TMPro.TMP_Text AmmoText;
	public GameObject gameOverPanel;
	public UnityEngine.UI.Image turboBar;
	public TMPro.TMP_Text shieldText;
	public GameObject dieText;
	public InventorySelector inventorySelector;
	public GameObject radialInventory;


	[Header("Level References")]
	public SpriteRenderer background;

	[Header("Level Objectives")]
	public int enemiesLeft;
	public bool endlessLevel = false;
	public bool bossLevel = false;
	public GameObject bossInThisLevel;


	[Header("Level Statistics:")]
	public int enemyCount;
	public TMPro.TMP_Text completedEnemyCount;

	public int cores;
	public TMPro.TMP_Text coresCollected;

	public TMPro.TMP_Text levelTime;

	float timestamp;

	bool bossSpawned = false;

	private void Awake()
	{
		background.sprite = SelectedLevel.Instance.GetLevel().backgroundImage;
		enemiesLeft = SelectedLevel.Instance.GetLevel().enemiesToShoot;
		bossLevel = SelectedLevel.Instance.IsBossLevel();
		if (enemiesLeft == 1) {
			endlessLevel = true;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		enemyCount = 0;
		timestamp = Time.time;


        Player.OnPlayerDeath += LevelCompleted;
        Player.OnPlayerLoaded += PlayerLoaded;

		Instantiate(playerPrefab, new Vector3(0, -4.35f, 0), Quaternion.identity);
	}

    private void OnDestroy()
    {
        Player.OnPlayerDeath -= LevelCompleted;
        Player.OnPlayerLoaded -= PlayerLoaded;
    }

    // Update is called once per frame
    void Update()
	{

	}



	public void PlayerLoaded(Player player)
	{
        Debug.Log("Player LOADED --> LevelMAnager");
		bossSpawned = false;
		player.gameSystemManager = gameSystemManager;
		player.AmmoText = AmmoText;
		player.gameOverPanel = gameOverPanel;
		player.turboBar = turboBar;
		player.shieldText = shieldText;
		player.dieText = dieText;
		player.inventorySelector = inventorySelector;
		player.radialInventory = radialInventory;

	}

	public void EnemyDestroyed()
	{
		enemyCount++;
		enemiesLeft--;
		if (enemiesLeft <= 0) {
			enemiesLeft = 0;
			if (!bossLevel) {
				LevelCompleted();
			} else {
				if (!bossSpawned)
					SpawnBoss();
				else
					Debug.Log("Boss Already Spawned!!");
			}
		}
	}

	public void SpawnBoss()
	{
		bossSpawned = true;
		if (enemySpawner == null) {
			enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
		}
		enemySpawner.SpawnBoss(SelectedLevel.Instance.GetBossList()[0]);
	}

	public void LevelCompleted()
	{
		Debug.Log("Level Completed GGWP");
		if (OnLevelCompleted != null)
			OnLevelCompleted();
		SelectedLevel.Instance.CompletteLevel();
		PlayerProgress.Instance.levelCompleted();
		float time = Time.time - timestamp;
		int minutes = Mathf.FloorToInt(time/60);
		int secounds = Mathf.FloorToInt(time - minutes * 60);
		levelTime.text = minutes.ToString("00") + " : " + secounds.ToString("00");
		completedEnemyCount.text = enemyCount.ToString();
		coresCollected.text = PlayerProgress.Instance.powerCoresThisLevel.ToString();
	}


	public void StartBossPhase()
	{

	}

	public void BossDestroyed()
	{
		LevelCompleted();
	}

}

