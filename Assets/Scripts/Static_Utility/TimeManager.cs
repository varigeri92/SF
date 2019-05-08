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
		player = GameObject.FindGameObjectWithTag("Player").transform;
        NormalizeTime();
	}


	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
            SlowTime(0.02f);
            playerPos = (Vector2)Camera.main.WorldToScreenPoint(player.position);
            inventoryUI.OpenMenu(playerPos);
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
            NormalizeTime();
            inventoryUI.CloseMenu();
        }
	}

    public void SlowTime(float timeScale)
    {
        Time.timeScale = slowAmount;
        Time.fixedDeltaTime = Time.timeScale * timeScale;
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
}
