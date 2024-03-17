using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItemTrigger : MonoBehaviour
{
    private GameObject _uiObject;

    private ItemData _itemData;

    [SerializeField]
    private bool _isPlayerInTrigger;

    private void Awake()
    {
        _itemData = GetComponent<ItemData>();
    }

    private void Update()    
    {

        if (_isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {

            InventoryManager.Instance.AssignItemData(_itemData);

            int infiniteLoopCount = 0;

            while (infiniteLoopCount<40) {

                if (_itemData.itemNumber <= 0)
                {
                    ForceToReturnUI();
                    Destroy(gameObject);
                    break;
                }

                int checkResult = InventoryManager.Instance.CheckInventory();

                //인스펙터 창의 데이터 업데이트
                InventoryManager.Instance.UpdateInspectorData();

                if (checkResult == 1) break;


                infiniteLoopCount++;

            }

        }

    }


    public void UpdateUIPosition(GameObject item, GameObject uiElement)
    {
    Vector3 itemPosition = item.transform.position; // 아이템의 월드 좌표
    Vector3 screenPosition = Camera.main.WorldToScreenPoint(itemPosition); // 월드 좌표를 스크린 좌표로 변환

    // UI 요소의 RectTransform을 가져와서 스크린 좌표로 이동
    RectTransform uiRectTransform = uiElement.GetComponent<RectTransform>();
    uiRectTransform.position = new Vector3(screenPosition.x + 200, screenPosition.y + 50, screenPosition.z);
    }

    public void ForceToReturnUI()
    {
        if(_uiObject is not null)
        {
            UIManager.Instance.ReturnUIObject(_uiObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //트리거 불 값 변환
            _isPlayerInTrigger = true;

            _uiObject = UIManager.Instance.GetUIObject();
            _uiObject.SetActive(true);

            UpdateUIPosition(gameObject, _uiObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_uiObject is not null)
        {
            UpdateUIPosition(gameObject, _uiObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") is true && _uiObject is not null)
        {

            //트리거 불 값 변환
            _isPlayerInTrigger = false;

            UIManager.Instance.ReturnUIObject(_uiObject);
        }
    }

}
