using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EquipmentUI : MonoBehaviour
{

    public bool isGun = true;

    [HideInInspector]
    protected Transform canvas;
    [HideInInspector]
    protected EquipmentUIManager equipmentUIManager;

    public UltimateSelector ultimateSelector;
    public UltimateSelector gunSelector;

    Animator animator;

    public GameObject marker;

    protected Button button;

    [Header("Asing by 'Ultimates' only:")]
    public Abilit_Object ultimate;




    // Start is called before the first frame update
    void Start()
    {

    }

    protected void Init()
    {

        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick());

        equipmentUIManager = GameObject.Find("EquipmentScreen").GetComponent<EquipmentUIManager>();

        ultimateSelector = GameObject.FindGameObjectWithTag("UltimateSelector").GetComponent<UltimateSelector>();
    }

    public virtual void OnButtonClick()
    {
        Debug.Log("Katt");
    }

    public void SetPalyerUltimate()
    {
        ultimateSelector.SetUltimate(ultimate);
    }

    public void SetMarker(bool state)
    {
        marker.SetActive(state);
    }

}
