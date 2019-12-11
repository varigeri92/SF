﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpType
{
	Gun,
	Shield
}

public class Player : MonoBehaviour
{
	public bool immortal;

	public delegate void PlayerDead();
	public static event PlayerDead OnPlayerDeath;

	public delegate void PlayerLoaded();
	public static event PlayerLoaded OnPlayerLoaded;


	public delegate void LevelUp(int level);
	public static event LevelUp OnLevelUp;


	public PlayerObject playerObject;
	public GameSystemManager gameSystemManager;

	public List<SelectorBehaviour> selectorBahaviours = new List<SelectorBehaviour>();
	public Dictionary<string, InventoryGun> gunsPickedUp = new Dictionary<string, InventoryGun>();
	//-----------------------------//
	public PiUIManager piUIManager;
	public PiUI piUI;

	[SerializeField]
	public Image xpFillImage;

	[SerializeField]
	int xpToNextLevel = 30;
	[SerializeField]
	int currentXp = 0;

	[SerializeField]
	int playerLevel = 1;



	string[] slices = { "One", "Two", "Three", "Four", "Five" };

	//-----------------------------//

	InventoryGun ActiveInventoryGun;

	public TMPro.TMP_Text enemyCounterText;
	public TMPro.TMP_Text AmmoText;
	public TMPro.TMP_Text ultimateChargesValue_Text;
	int enemyCounter = 0;

	int activeGunAmmo;

	public GameObject gameOverPanel;
	public GameObject playerExplosion;
	public GameObject basicGun;
	public GameObject playerForceShield;
	public float turbo;
	public Image turboBar;
	public TMPro.TMP_Text shieldText;
	public bool allowTurbo;
	public GameObject dieText;
	public float speed = 3f;
	float _moveSpeed;
	public List<Transform> attachPoints = new List<Transform>();
	public List<Gun> activeGuns = new List<Gun>();

	public Transform abilityAtachPoint;
	public Ability ability;

	public int health = 10;
	float horizontal = 0f;
	float vertical = 0f;

	public bool jetMode;

	public float lookSpeed;

	// public bool useController;

	public InventorySelector inventorySelector;
	public GameObject radialInventory;

	bool hasShield = false;

	void Awake()
	{
		health = playerObject.maxHealth;
		inventorySelector = GameObject.FindGameObjectWithTag("LOGIC").GetComponentInChildren<InventorySelector>();
        InputManager.OnStart();

        InputManager.OnShootButtonPresed += ShootEvent;
        InputManager.OnShootButtonreleased += StopShoot; 
        InputManager.OnUltimateButtonPressed += UseAbility;
        InputManager.OnBoostButtonPressed += StartTurbo;
        InputManager.OnBoostButtonReleased += StopTurbo;

    }

    void onDestroy()
    {
        BasicEnemy.onEnemyDead -= countEnemyes;
        InputManager.OnShootButtonPresed -= ShootEvent;
        InputManager.OnShootButtonreleased -= StopShoot;
        InputManager.OnUltimateButtonPressed -= UseAbility;
        InputManager.OnBoostButtonPressed -= StartTurbo;
        InputManager.OnBoostButtonReleased -= StopTurbo;
    }

    void Start()
	{
		GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().OnPlayerLoaded(this);
		SetBasicGun();
		BasicEnemy.onEnemyDead += countEnemyes;
		shieldText.text = health.ToString();

		if(playerObject.normalPlayer){
			GameObject.FindGameObjectWithTag("XPandLVL").SetActive(false);
		}

		SetPlayerUltimate();

        speed = playerObject.speed;
        _moveSpeed = playerObject.speed;

		if (OnPlayerLoaded != null)
			OnPlayerLoaded();
	}

	void countEnemyes(BasicEnemy enemy)
	{
		enemyCounter++;
		LevelingProgress(enemy.enemyObject.xp);
	}

	void UseAbility(){
		if(ability == null){
			ability = abilityAtachPoint.GetComponentInChildren<Ability>();
		}
		ability.FireAbility();
	}


	void Die()
	{
#if UNITY_EDITOR
		if (immortal)
			return;
#endif

		dieText.SetActive(true);
		if (OnPlayerDeath != null) {
			OnPlayerDeath();
		}

		Instantiate(playerExplosion, transform.position, Quaternion.identity);
		gameOverPanel.SetActive(true);
		TimeManager timeManager = GameObject.FindGameObjectWithTag("LOGIC").GetComponentInChildren<TimeManager>();
		timeManager.SlowTime(0.05f);
		timeManager.StartAutoSet();
		Destroy(gameObject);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q)){
			UseAbility();
		}

		if (gameSystemManager.isPaused) {
			return;
		}

        InputManager.OnUpdate();

		if (InputManager.usingController) {
			LookRightStick();
		} else {
			LookAtMouse();
		}

		Moove(InputManager.GetMovement());

        if (!jetMode)
        {
            CountTurbo(playerObject.boostFillSpeed);
        }
        else 
        {
            CountTurbo(-playerObject.boostDecSpeed);
        }

	}

    void StartTurbo()
    {
        Debug.Log("START TURBO!!");
        if (allowTurbo)
        {
           _moveSpeed = speed * 2;
            jetMode = true;
        }
    }

    void StopTurbo()
    {
        Debug.Log("STOP TURBO!!");
        _moveSpeed = speed;
        jetMode = false;
    }

	void CountTurbo(float mult)
	{
		if (turbo < 1 | turbo > 0f) {
			turbo += mult * Time.deltaTime;
			turboBar.fillAmount = turbo;
			if (turbo > 0.5f)
				allowTurbo = true;
		}

		if (turbo > 1) {
			turbo = 1;
			turboBar.fillAmount = turbo;
		} else if (turbo < 0) {
			turbo = 0;
			turboBar.fillAmount = turbo;
			allowTurbo = false;
		}

	}

	public void TakeDmg(int dmg)
	{
		Debug.Log("Player Takes: " + dmg + " damage");
		health -= dmg;
		if (health <= 0) {
			Die();
		}
		if (health >= 0) {
			shieldText.text = health.ToString();
		} else {
			shieldText.text = "0";
		}
	}
	void Shoot()
	{
		if (Input.GetAxis("Fire1") > 0.1) {
			foreach (Gun gun in activeGuns) {
				if (gun != null) {

					gun.Shooting(true);
					if (gun.ammo != -999) {
						AmmoText.text = gun.ammo.ToString();
					} else {
						AmmoText.text = "INFINITE";
					}
				} else {
					OnGunChanged();
				}
			}
		} else {
			foreach (Gun gun in activeGuns) {
				if (gun != null) {

					gun.StopShooting();
				} else {
					OnGunChanged();
				}
			}
		}
	}

    void StopShoot()
    {
        Debug.Log("Stop_ShootingEvent");
        foreach (Gun gun in activeGuns)
        {
            if (gun != null)
            {

                gun.StopShooting();
            }
            else
            {
                OnGunChanged();
            }
        }
    }

    void ShootEvent()
    {
        Debug.Log("ShootingEvent");
        foreach (Gun gun in activeGuns)
        {
            if (gun != null)
            {

                gun.Shooting(true);
                if (gun.ammo != -999)
                {
                    AmmoText.text = gun.ammo.ToString();
                }
                else
                {
                    AmmoText.text = "INFINITE";
                }
            }
            else
            {
                OnGunChanged();
            }
        }
    }

	public void OnGunChanged()
	{
		activeGuns = new List<Gun>();
		foreach (Transform t in attachPoints) {
			if (t.GetComponentInChildren<Gun>() != null) {
				activeGuns.Add(t.GetComponentInChildren<Gun>());
			}
		}
	}

	private void Moove(Vector3 direction)
	{
		float dist = Vector3.Distance(Input.mousePosition, Camera.main.WorldToScreenPoint(transform.position));
		float _speed;

		if (dist < 20) {
			_speed = 0;
		} else {
			_speed = _moveSpeed;
		}
		transform.position = Vector3.Lerp(transform.position, transform.position + direction * _speed, Time.deltaTime);
		// transform.Translate(direction * _speed * Time.deltaTime);
	}

	void LookAtMouse()
	{
		Vector3 dir = (Vector3)InputManager.GetMousePosition() - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
	}

	void LookRightStick()
	{
		// float direction = inputManager.GetDirection().x * inputManager.GetDirection().y;
		if (InputManager.GetDirection().x != 0f && InputManager.GetDirection().y != 0f) {

			float angle = Mathf.Atan2(InputManager.GetDirection().x, InputManager.GetDirection().y) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}
	}

	public void PickUpShield(int shield)
	{
		health += shield;
		shieldText.text = health.ToString();
	}

	public void PickupForceShield()
	{
		if (transform.GetComponentInChildren<FX_Shield>() != null) {
			Destroy(transform.GetComponentInChildren<FX_Shield>().gameObject);
		}
		Instantiate(playerForceShield, transform);
	}

	public void PickupGun(GameObject gun, GameObject icon)
	{
		bool alreadyGot = false;
		if (!alreadyGot) {
			if (inventorySelector.items.Contains(gun)) {

				Gun activeGun = attachPoints[0].GetChild(0).GetComponent<Gun>();
				string activeGunName = activeGun.gameObject.name.Replace("(Clone)", "");
				if (gun.name == activeGunName) {
					Debug.Log("Add ammo for active Gun: " + activeGunName);
					// activeGun.ammo += gun.GetComponent<Gun>().ammo;
					activeGun.AddAmmo(gun.GetComponent<Gun>().ammo);
					AmmoText.text = activeGun.ammo.ToString();
				} else {
					Debug.Log("Ammo picked up for: " + gun.name);
					gunsPickedUp[gun.name].AddAmmo(gun.GetComponent<Gun>().ammo);
				}
			} else {
				inventorySelector.items.Add(gun);
				inventorySelector.icons.Add(icon);
				GameObject inventoryIcon = 
					Instantiate(icon, radialInventory.transform.Find(slices[inventorySelector.items.Count - 1]).GetChild(0));
				if (inventoryIcon.GetComponent<InventoryGun>() != null) {
					InventoryGun invGun = inventoryIcon.GetComponent<InventoryGun>();
					invGun.SetAmmo(gun.GetComponent<Gun>().ammo);
					gunsPickedUp.Add(gun.name, invGun);

				} else {
					Debug.LogWarning("No 'InventoryGun' component found on: " + inventoryIcon.name);
				}
			}
		}
	}

	public void LoadSavedInventory(){
		int length = inventorySelector.items.Count;
		for (int i = 1; i < length; i++) {
			Instantiate(inventorySelector.icons[i], radialInventory.transform.GetChild(i).GetChild(0));
			gunsPickedUp.Add(inventorySelector.items[i].name, inventorySelector.icons[i].GetComponent<InventoryGun>());
		}
	}

	void SetBasicGun()
	{
		activeGuns = new List<Gun>();
		foreach (Transform t in attachPoints) {
			if (t.GetComponentInChildren<Gun>() != null) {
				Gun gun = t.GetComponentInChildren<Gun>();
				activeGuns.Add(gun);
				PickupGun(basicGun, gun.icon);
			}
		}
	}

	public void ChangeGun(GameObject go)
	{

        Gun activeGun = attachPoints[0].GetChild(0).GetComponent<Gun>();
        string activeGunName = activeGun.gameObject.name.Replace("(Clone)", "");
        if (go.name == activeGun.gameObject.name)
        {
            Debug.Log("Already on this Gun");
        }
        else
        {
            if (gunsPickedUp.ContainsKey(activeGunName))
            {
				Debug.Log("Set Remaining ammo for old Gun: " + activeGunName + "Ammo: " + activeGun.ammo);
                gunsPickedUp[activeGunName].SetAmmo(activeGun.ammo);
            }
            Destroy(activeGun.gameObject);

            Gun newGun = Instantiate(go, attachPoints[0]).GetComponent<Gun>();
           if(newGun.ammo != -999)
            {

				newGun.ammo = gunsPickedUp[go.name].GetAmmo();
                // Debug.Log("New Gun Ammo: " + newGun.ammo);
                AmmoText.text = newGun.ammo.ToString();
            }
            else {
                AmmoText.text = "INFINITE";
            }

		    OnGunChanged();
        }
	}

    void LevelingProgress(int xp)
    {
		if(playerObject.normalPlayer){
			return;
		}

        currentXp += xp;
        if(currentXp >= xpToNextLevel)
        {
            playerLevel++;
            if (OnLevelUp != null)
            {
                OnLevelUp(playerLevel);
            }
            enemyCounterText.text = playerLevel.ToString();
            xpFillImage.fillAmount = 0f;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);
            currentXp = 0;
        }
        else
        {
            float progress = (float)currentXp / (float)xpToNextLevel;

            if (xpFillImage == null)
                xpFillImage = GameObject.FindGameObjectWithTag("XpFillImage").GetComponent<Image>();

            xpFillImage.fillAmount = progress;
        }
    }

	void SetPlayerUltimate(){
		Transform t = GameObject.FindGameObjectWithTag("UltimatePanel").transform;
		Transform ultimateContainer = t.GetChild(0);

		if(playerObject.ultimate == null)
		{
			t.gameObject.SetActive(false);
		}
		else
		{
			if(abilityAtachPoint.childCount > 0 ){
				Destroy(abilityAtachPoint.GetChild(0).gameObject);
			}
			GameObject go = Instantiate(playerObject.ultimate, abilityAtachPoint);
			ability = go.GetComponent<Ability>();
			Instantiate(playerObject.ultimateIcon, ultimateContainer);
		}
	}

}
