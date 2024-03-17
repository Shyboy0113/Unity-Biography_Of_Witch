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

                //�ν����� â�� ������ ������Ʈ
                InventoryManager.Instance.UpdateInspectorData();

                if (checkResult == 1) break;


                infiniteLoopCount++;

            }

        }

    }


    public void UpdateUIPosition(GameObject item, GameObject uiElement)
    {
    Vector3 itemPosition = item.transform.position; // �������� ���� ��ǥ
    Vector3 screenPosition = Camera.main.WorldToScreenPoint(itemPosition); // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ

    // UI ����� RectTransform�� �����ͼ� ��ũ�� ��ǥ�� �̵�
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
            //Ʈ���� �� �� ��ȯ
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

            //Ʈ���� �� �� ��ȯ
            _isPlayerInTrigger = false;

            UIManager.Instance.ReturnUIObject(_uiObject);
        }
    }

}
