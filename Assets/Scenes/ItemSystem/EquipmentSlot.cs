using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlot : MonoBehaviour
{
    public Image itemImage;

    public ItemData itemData;

    private void Awake()
    {
        InventoryManager.Instance.CheckInstance();
        GetEquipmentData();
    }

    private void OnEnable()
    {
        GetEquipmentData();
    }

    public void GetEquipmentData()
    {
        int idx = transform.GetSiblingIndex();

        itemData = InventoryManager.Instance.GetEquipmentIndexData(idx);
        

    }
}
