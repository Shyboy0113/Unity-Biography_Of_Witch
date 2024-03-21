using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum SlotType //마우스 포인터로 인식되는 인벤토리 타입
{
    Null,
    Inventory,
    Equipment,
    Weapon    
}

public class InventoryMouseEvent : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    //아이템 스왑용 변수
    public SlotType slotType;
    public int slotIdx;

    //아이템 정보창 전용 변수
    private Image _itemImage;
    private ItemData _itemData;

    private void Awake()
    {
        GetItemData();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //인벤토리/장비창 외부를 클릭했을 경우 드래그 취소
            if (InventoryManager.Instance.isDragNDrop
                && !InventoryManager.Instance.targetData)
            {
                InventoryManager.Instance.DeleteDragData();
            }
        }        
    }

    public bool IsNull()
    {
        switch (slotType)
        {
            case SlotType.Inventory:
                if (!InventoryManager.Instance.inventoryToggle[slotIdx]) return true;
                break;
            case SlotType.Equipment:
                if (!InventoryManager.Instance.equipmentToggle[slotIdx]) return true;
                break;
            case SlotType.Weapon:
                if (!InventoryManager.Instance.weaponToggle[slotIdx]) return true;
                break;
        }

        if (_itemData.itemCode == "0000" && _itemData.itemNumber == 0) return true;

        return false;

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
                if (InventoryManager.Instance.isDragNDrop == false && (!IsNull()))
                {
                    InventoryManager.Instance.targetImage = _itemImage;

                    InventoryManager.Instance.StartDragEvent();
                }
                else
                {
                    InventoryManager.Instance.EndDragEvent();
                }
            }
            // 우클릭을 감지
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!IsNull())
                {
                    int idx;

                    switch (slotType)
                    {
                        case SlotType.Inventory:
                            if (EquipmentType.Head <= _itemData.itemType && _itemData.itemType <= EquipmentType.Earrings)
                            {
                                InventoryManager.Instance.ItemSwap(ref InventoryManager.Instance.inventorySlots[slotIdx], ref InventoryManager.Instance.equipmentSlots[(int)_itemData.itemType]);
                                InventoryManager.Instance.ItemBoolSwap(ref InventoryManager.Instance.inventoryToggle[slotIdx], ref InventoryManager.Instance.equipmentToggle[(int)_itemData.itemType]);
                            }
                            else if (_itemData.itemType == EquipmentType.Weapon)
                            {
                                idx = InventoryManager.Instance.FindWeaponSlotBlank();

                                if (idx == -1) //빈칸이 없을 경우 반드시 첫 칸과 교환함
                                {
                                    InventoryManager.Instance.ItemSwap(ref InventoryManager.Instance.inventorySlots[slotIdx], ref InventoryManager.Instance.weaponSlots[idx]);
                                    InventoryManager.Instance.ItemBoolSwap(ref InventoryManager.Instance.inventoryToggle[slotIdx], ref InventoryManager.Instance.weaponToggle[idx]);
                                }
                                else
                                {
                                    InventoryManager.Instance.ItemSwap(ref InventoryManager.Instance.inventorySlots[slotIdx], ref InventoryManager.Instance.weaponSlots[0]);
                                    InventoryManager.Instance.ItemBoolSwap(ref InventoryManager.Instance.inventoryToggle[slotIdx], ref InventoryManager.Instance.weaponToggle[0]);
                                }

                            }

                            break;

                        case SlotType.Equipment:

                            idx = InventoryManager.Instance.FindBlank();
                            if (idx != -1)
                            {
                                InventoryManager.Instance.ItemSwap(ref InventoryManager.Instance.inventorySlots[idx], ref InventoryManager.Instance.equipmentSlots[slotIdx]);
                                InventoryManager.Instance.ItemBoolSwap(ref InventoryManager.Instance.inventoryToggle[idx], ref InventoryManager.Instance.equipmentToggle[slotIdx]);
                            }

                            break;

                        case SlotType.Weapon:

                            idx = InventoryManager.Instance.FindBlank();
                            if (idx != -1)
                            {
                                InventoryManager.Instance.ItemSwap(ref InventoryManager.Instance.inventorySlots[idx], ref InventoryManager.Instance.weaponSlots[slotIdx]);
                                InventoryManager.Instance.ItemBoolSwap(ref InventoryManager.Instance.inventoryToggle[idx], ref InventoryManager.Instance.weaponToggle[slotIdx]);
                            }

                            break;
                    }
                }
            }
        
    }

}
