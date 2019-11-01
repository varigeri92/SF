using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

	public delegate void PlayerDead();
	public static event PlayerDead OnLevelCompleted;

	public GameObject playerPrefab;

	[Header("Player References")]

	public GameSystemManager gameSystemManager;
	//-----------------------------//
	[SerializeField]
	UnityEngine.UI.Image xpFillImage;
	public TMPro.TMP_Text enemyCounterText;
	public TMPro.TMP_Text AmmoText;
	public GameObject gameOverPanel;
	public UnityEngine.UI.Image turboBar;
	public TMPro.TMP_Text shieldText;
	public GameObject dieText;
	public InputManager inputManager;
	public InventorySelector inventorySelector;
	public GameObject radialInventory;


	[Header("Level References")]
	public SpriteRenderer background;

	[Header("Level Objectives")]
	public int enemiesLeft;
	public bool endlessLevel = false;

	private void Awake()
	{
		background.sprite = SelectedLevel.Instance.GetLevel().backgroundImage;
		enemiesLeft = SelectedLevel.Instance.GetLevel().enemiesToShoot;
		if(enemiesLeft == 1){
			endlessLevel = true;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		Instantiate(playerPrefab,new Vector3(0, -4.35f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



	public void OnPlayerLoaded(Player player){
		Camera.main.GetComponent<FollowPlayer>().playerTransform = player.transform;
		Camera.main.GetComponent<FollowPlayer>().InitCam();
		player.gameSystemManager = gameSystemManager;
		player.xpFillImage = xpFillImage;
		player.enemyCounterText = enemyCounterText;
		player.AmmoText = AmmoText;
		player.gameOverPanel = gameOverPanel;
		player.turboBar = turboBar;
		player.shieldText = shieldText;
		player.dieText = dieText;
		player.inputManager = inputManager;
		player.inventorySelector = inventorySelector;
		player.radialInventory = radialInventory;

		// Destroy(this);
	}

	public void EnemyDestroyed(){
		enemiesLeft--;
		if(enemiesLeft <= 0){
			enemiesLeft = 0;
			LevelCompleted();
		}
	}

	public void LevelCompleted(){
		Debug.Log("Level Completed GGWP");
		if(OnLevelCompleted != null && !endlessLevel)
			OnLevelCompleted();
	}

	public void StartBossPhase(){
		
	}


}
