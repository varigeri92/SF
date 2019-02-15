using System.Collections;
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
	public delegate void PlayerDead();
	public static event PlayerDead OnPlayerDeath;

	public List<SelectorBehaviour> selectorBahaviours = new List<SelectorBehaviour>();

	public TMPro.TMP_Text enemyCounterText;
	public TMPro.TMP_Text AmmoText;
	int enemyCounter = 0;

	public GameObject basicGun;
	public float turbo;
	public Image turboBar;
	public TMPro.TMP_Text shieldText;
	public bool allowTurbo;
	public GameObject dieText;
	public float speed = 3f;
	float _moveSpeed;
	public List<Transform> attachPoints = new List<Transform>();
	public List<Gun> activeGuns = new List<Gun>();

	public int health = 10;
	float horizontal = 0f;
	float vertical = 0f;

	public bool jetMode;

	public InputManager inputManager;
	public float lookSpeed;

	public bool useController;

	public InventorySelector inventorySelector;
	public GameObject radialInventory;
	void Awake(){
		inventorySelector = GameObject.FindGameObjectWithTag("LOGIC").GetComponentInChildren<InventorySelector>();
		Debug.Log(inventorySelector.name);
	}
	void Start()
	{
		SetBasicGun();
		BasicEnemy.onEnemyDead += countEnemyes;
		

	}
	void countEnemyes(BasicEnemy enemy)
	{
		enemyCounter++;
		enemyCounterText.text = enemyCounter.ToString();

	}
	void onDestroy(){
		BasicEnemy.onEnemyDead -= countEnemyes;
	}
	void Die()
	{
		dieText.SetActive(true);
		if (OnPlayerDeath != null) {
			OnPlayerDeath();
		}
		Destroy(gameObject);
	}
	void Update()
	{
		
		if (useController)
		{
			LookRightStick();
		}
		else
		{
			LookAtMouse();
		}

		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		if (Input.GetButton("Jump") && allowTurbo) {
			_moveSpeed = speed * 2;
			CountTurbo(-0.5f);
			jetMode = true;
		} else if (Input.GetButtonUp("Jump")) {
			_moveSpeed = speed;
			jetMode = false;
		} else {
			jetMode = false;
			_moveSpeed = speed;
			CountTurbo(0.2f);
		}

		Vector3 direction = new Vector3(horizontal, vertical, 0);
		Moove(direction);

		Shoot();
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
		health -= dmg;
		if (health < 0) {
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
					if (gun.ammo != -999){
						AmmoText.text = gun.ammo.ToString();
					}
					else
					{
						AmmoText.text = "∞";
					}
				} else {
					OnGunChanged();
				}
			}
		}
	}

	public void OnGunChanged()
	{
		activeGuns = new List<Gun>();
		foreach (Transform t in attachPoints) {
			if (t.GetComponentInChildren<Gun>() != null) {
				Debug.Log(GetComponentInChildren<Gun>().name);
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
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
	}

	void LookRightStick()
	{
		// float direction = inputManager.GetDirection().x * inputManager.GetDirection().y;
		if(inputManager.GetDirection().x != 0f && inputManager.GetDirection().y != 0f){
			
		float angle = Mathf.Atan2(inputManager.GetDirection().x, inputManager.GetDirection().y) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}
	}

	public void PickUpShield(int shield)
	{
		health += shield;
		shieldText.text = health.ToString();
	}
	/* 
	public void PickupGun(GameObject gun, GameObject icon)
	{
		bool alreadyGot = false;
		Debug.Log("PickupCalled");

		if (!alreadyGot) {
			foreach (SelectorBehaviour selector in selectorBahaviours) {
				if (!selector.haschild) {
					
					selector.gun = gun;
					selector.haschild = true;
					Instantiate(icon, selector.gameObject.transform);
					break;
				} else if (selector.gun.name == gun.name) {
					Debug.Log("ADD AMMO??");
					selector.gun.GetComponent<Gun>().ammo += 100;
					break;
				}
			}
		}
	}
	*/

	public void PickupGun(GameObject gun, GameObject icon)
	{
		bool alreadyGot = false;
		if (!alreadyGot) {
			if(inventorySelector.items.Contains(gun)){
				Debug.Log("ADD AMMO!");
				return;
			}
			inventorySelector.items.Add(gun);
			inventorySelector.icons.Add(icon);
			
			Instantiate(icon, radialInventory.transform.GetChild(inventorySelector.items.Count -1 ).GetChild(0));				
		
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
		
		Destroy(attachPoints[0].GetChild(0).gameObject);
		Instantiate(go, attachPoints[0]);
		OnGunChanged();
	}
}
