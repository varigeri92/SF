using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EquipmentButton : MonoBehaviour
{

    [HideInInspector]
    protected EquipmentUIManager equipmentUIManager;

    Animator animator;

    public GameObject marker;

    protected Button button;


    protected void Init()
    {
        //SetMarker(true);
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick());

        equipmentUIManager = GameObject.Find("EquipmentScreen").GetComponent<EquipmentUIManager>();

    }

    public virtual void OnButtonClick()
    {
        Debug.Log("Katt");
    }



    public void SetMarker(bool state)
    {
        marker.SetActive(state);
    }

}
