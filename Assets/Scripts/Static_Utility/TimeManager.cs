using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public PiUI inventoryUI;

	[SerializeField]
	float slowAmount;
	Vector2 playerPos;
	Transform player;
	float _fixedDeltaTime;
	private void Start()
	{
		_fixedDeltaTime = Time.fixedDeltaTime;
		// player = GameObject.FindGameObjectWithTag("Player").transform;
        NormalizeTime();
		Player.OnPlayerLoaded += Player_OnPlayerLoaded;
        InputManager.OnInventoryButtonPressed += OpenInventory;
        InputManager.OnInventoryButtonReleased += CloseInventory;
    }

    void OpenInventory()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        SlowTime(0.02f);
        playerPos = (Vector2)Camera.main.WorldToScreenPoint(player.position);
        inventoryUI.OpenMenu(playerPos);
    }

    void CloseInventory()
    {
        NormalizeTime();
        inventoryUI.CloseMenu();
    }


	void Update () {

	}

    public void SlowTime(float timeScale)
    {
        Time.timeScale = slowAmount;
        Time.fixedDeltaTime = Time.timeScale * timeScale;
    }

	public void SlowTime()
	{
		Time.timeScale = slowAmount;
		Time.fixedDeltaTime = Time.timeScale * 0.1f;
	}

    public void NormalizeTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = _fixedDeltaTime;
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }
    public void StartAutoSet()
    {
        StartCoroutine(AutoSetTime());
    }

    IEnumerator AutoSetTime()
    {
        yield return new WaitForSecondsRealtime(5f);
        NormalizeTime();
    }

	void Player_OnPlayerLoaded()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		LevelManager.OnLevelCompleted += SlowTime;
	}

	private void OnDestroy()
	{
		Player.OnPlayerLoaded -= Player_OnPlayerLoaded;
		LevelManager.OnLevelCompleted -= SlowTime;
        InputManager.OnInventoryButtonPressed -= OpenInventory;
        InputManager.OnInventoryButtonReleased -= CloseInventory;
    }
}
