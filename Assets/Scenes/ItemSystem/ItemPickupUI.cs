using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupUI : MonoBehaviour
{
    private GameObject uiObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            uiObject = UIManager.Instance.GetUIObject();
            uiObject.SetActive(true);
            uiObject.transform.position = transform.position + new Vector3(0, 2, 0); // 아이템 위에 위치
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") is true && uiObject is not null)
        {
            UIManager.Instance.ReturnUIObject(uiObject);
        }
    }

}
