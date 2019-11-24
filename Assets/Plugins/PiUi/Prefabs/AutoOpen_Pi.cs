using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpen_Pi : MonoBehaviour
{
	public Vector2 piPosition;
    // Start is called before the first frame update
    void Start()
    {
		piPosition = new Vector2(Screen.width * 0.75f, Screen.height * 0.5f);
		GetComponent<PiUI>().OpenMenu(piPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
