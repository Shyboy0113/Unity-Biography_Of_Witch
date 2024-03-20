using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Head, //0
    Top, //1
    Bottom, //2
    Glove, //3
    Shoes, //4
    Necklace, //5
    Ring, //6
    Earrings, //7
    Weapon, //8
    Null //9

}

public class ItemData : MonoBehaviour
{       

    public string itemCode = "0000";
    public int itemNumber = 1;
    public EquipmentType itemType = EquipmentType.Null;

    public void AddItem(int num)
    {
        itemNumber += num;

    }
    public void RemoveItem(int num)
    {
        itemNumber -= num;
    }

}
