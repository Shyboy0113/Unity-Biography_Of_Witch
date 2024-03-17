using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{       

    public string itemCode = "0000";
    public int itemNumber = 1;

    public void AddItem(int num)
    {
        itemNumber += num;

    }
    public void RemoveItem(int num)
    {
        itemNumber -= num;
    }

}
