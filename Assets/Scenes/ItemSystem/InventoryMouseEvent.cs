using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum SlotType
{
    Null,
    Inventory,
    Equipment,
    Weapon    
}

public class InventoryMouseEvent : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{

    //������ ���ҿ� ����
    public SlotType slotType;
    public int slotIdx;

    //������ ����â ���� ����
    private Image _itemImage;
    private ItemData _itemData;

    private void Awake()
    {
        GetItemData();
    }

    private void OnDisable()
    {
        InventoryManager.Instance.targetData = null;
        InventoryManager.Instance.DeleteDragData();
    }        

    public void GetItemData()
    {
        _itemImage = GetComponent<Image>();

        int slotIdx = transform.GetSiblingIndex();

        switch (slotType)
        {
            case SlotType.Inventory:
                _itemData = InventoryManager.Instance.GetInventoryIndexData(slotIdx);
                break;
            case SlotType.Equipment:
                _itemData = InventoryManager.Instance.GetEquipmentIndexData(slotIdx);
                break;
            case SlotType.Weapon:
                _itemData = InventoryManager.Instance.GetWeaponIndexData(slotIdx);
                break;
        }        
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_itemData != null)
        {
            InventoryManager.Instance.targetData = _itemData;
            InventoryManager.Instance.targetSlotType = slotType;
            InventoryManager.Instance.targetSlotIdx = slotIdx;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.targetData = null;
        InventoryManager.Instance.targetSlotType = SlotType.Null;
        InventoryManager.Instance.targetSlotIdx = -1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryManager.Instance.isDragNDrop == false)
            {
                InventoryManager.Instance.targetImage = _itemImage;

                InventoryManager.Instance.StartDragEvent();
            }
            else
            {
                InventoryManager.Instance.EndDragEvent();
            }
        }
        // ��Ŭ���� ����
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("��Ŭ����!");
        }
    }

}
