using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HowerText : MonoBehaviour
{
	[SerializeField]
	TMP_Text description;

	[SerializeField]
	TMP_Text tittleShadow;
	[SerializeField]
	TMP_Text tittle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = Input.mousePosition;
    }

	public void SetDescription(string _description){
		description.text = _description;
	}

	public void SetTittle(string _tittle){
		tittleShadow.text = _tittle;
		tittle.text = _tittle;
	}
}
