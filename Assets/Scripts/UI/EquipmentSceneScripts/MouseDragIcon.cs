using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDragIcon : MonoBehaviour
{

	public Image icon;
	bool inDragState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(inDragState){
			transform.position = Input.mousePosition;
		}
    }

	public void SetData(Sprite iconSprite){
		icon.sprite = iconSprite;
		inDragState = true;
	}
}
