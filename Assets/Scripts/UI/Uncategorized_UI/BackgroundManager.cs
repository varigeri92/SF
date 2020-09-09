using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	Vector3 direction;
	public Player player;
	public SpriteRenderer _renderer;
	Material material;
	float mat_x;
	float mat_y;
    float speed = 0;
	// Use this for initialization
	void Start () {
		material = _renderer.material;

	}
	
	// Update is called once per frame
	void Update () {
		
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		mat_x += horizontal * Time.deltaTime;
		mat_y += vertical * Time.deltaTime;
		mat_x = Mathf.Clamp(mat_x, -1f, 1f);
		mat_y = Mathf.Clamp(mat_y, -1f, 1f);
		material.SetFloat("_Vertical",mat_y*0.01f);
		material.SetFloat("_Horizontal", mat_x * 0.01f);

        speed += normalizeSpeed(0.01f, horizontal, vertical);
        if (speed > 2)
        {
            speed = 2;
        }
        material.SetFloat("_Speed", speed);
    }

    float normalizeSpeed(float value, float horizontal, float vertical)
    {
        float normalizedSpeed = 1;

        if (horizontal > 0)
        {

            normalizedSpeed = horizontal * vertical +1;
            
        }else if (horizontal < 0)
        {
             normalizedSpeed = horizontal * vertical +1;

        }
        normalizedSpeed = Mathf.Abs(normalizedSpeed);
        return normalizedSpeed * Time.deltaTime * 0.1f;
    } 
}
