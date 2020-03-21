using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpen_Pi : MonoBehaviour
{
	public Vector2 piPosition;
    [SerializeField] private Vector2 screenPercentage;
    // Start is called before the first frame update
    void Start()
    {
        piPosition = new Vector2(Screen.width * screenPercentage.x, Screen.height * screenPercentage.y);
		GetComponent<PiUI>().OpenMenu(piPosition);
    }

    private void OnEnable()
    {
        //piPosition = new Vector2(Screen.width * screenPercentage.x, Screen.height * screenPercentage.y);
        //GetComponent<PiUI>().OpenMenu(piPosition);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
