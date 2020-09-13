using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunEquipmentButton : EquipmentUI
{

    [SerializeField] GunObject gunObject;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.parent.Find("PIBG").GetComponent<Animator>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPiOnSelection()
    {
        animator.SetBool("Open", true);
        equipmentUIManager.SetSelectedGun(gunObject);
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();
        OpenPiOnSelection();
    }

}
