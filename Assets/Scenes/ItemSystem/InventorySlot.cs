using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public TMP_Text itemNumber;
    public Image itemImage;

    public ItemData itemData;

    private void Awake()
    {
        InventoryManager.Instance.CheckInstance();
        GetInventoryData();
    }

    private void OnEnable()
    {
        GetInventoryData();
        InventoryManager.Instance.OnInventoryChanged += GetInventoryData;
    }

    private void OnDisable()
    {
        InventoryManager.Instance.OnInventoryChanged -= GetInventoryData;
    }

    public void GetInventoryData()
    {
        int idx = transform.GetSiblingIndex();
                
        itemData = InventoryManager.Instance.GetInventoryIndexData(idx);

        if (itemData != null)
        {
            if (itemData.itemNumber == 0) itemNumber.text = "" ;
            else itemNumber.text = itemData.itemNumber.ToString();

        }

    }

}
