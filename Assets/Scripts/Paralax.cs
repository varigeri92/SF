using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

	public Camera cam;
	public float Speed = 0.025f;

	public float camPosX;
	public float camPosY;


	public float verticalBounds;
	public float horizontalBonds;

	Vector3 LastCameraPosition;

	public Transform[] tiles;

    // Start is called before the first frame update
    void Start()
    {
		camPosX = 0;
		camPosY = 0;

		SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		verticalBounds = spriteRenderer.bounds.size.y;
		horizontalBonds = spriteRenderer.bounds.size.x;

		LastCameraPosition = cam.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
		Vector3 direction = LastCameraPosition - cam.transform.position;

		transform.position += direction * Speed;	

		LastCameraPosition = cam.transform.position;



		if(cam.transform.position.x * Speed > horizontalBonds + camPosX){
			// camPosX += horizontalBonds;
			camPosX = cam.transform.position.x* Speed;
			transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
		}

		if (cam.transform.position.x * Speed < camPosX - horizontalBonds) {
			// camPosX -= horizontalBonds;
			camPosX = cam.transform.position.x* Speed;
			transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
		}


		if (cam.transform.position.y * Speed > verticalBounds + camPosY) {
			// camPosY += verticalBounds;
			camPosY = cam.transform.position.y* Speed;
			transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
		}

		if (cam.transform.position.y * Speed < camPosY - verticalBounds) {
			// camPosY -= verticalBounds;
			camPosY = cam.transform.position.y* Speed;
			transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
		}


    }
}
