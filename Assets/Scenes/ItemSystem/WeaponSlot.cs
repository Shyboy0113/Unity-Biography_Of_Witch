using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    public Image itemImage;

    public ItemData itemData;

    private void Awake()
    {
        InventoryManager.Instance.CheckInstance();
        GetWeaponData();
    }

    private void OnEnable()
    {
        GetWeaponData();
    }

    public void GetWeaponData()
    {
        int idx = transform.GetSiblingIndex();
        
        itemData = InventoryManager.Instance.GetWeaponIndexData(idx);


    }
}
