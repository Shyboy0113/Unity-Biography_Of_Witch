using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [System.Serializable]
    public struct InventorySlotData
    {
        public string itemCode;
        public int itemNumber;
        public string itemType;
    }

    [Header("����â ������")]
    public ItemData[] weaponSlots;
    public bool[] weaponToggle;

    [Header("���â ������")]
    public ItemData[] equipmentSlots;
    public bool[] equipmentToggle;

    [Header("�κ��丮 ������ ����")]
    public InventorySlotData[] inventorySlotData;

    [Header("�κ��丮 ������")]
    public ItemData[] inventorySlots;
    public bool[] inventoryToggle;

    [Header("Ʈ���� ������")]
    //ItemTrigger���� ���޹��� �������� ������
    public ItemData dropItemData;

    [Header("�κ��丮 GameObject ��ġ")]
    public Transform inventorySlotParent;
    public Transform equipmentSlotParent;
    public Transform weaponSlotParent;

    //�巡�� �� ��� ���� ����
    public ItemData targetData;
    public SlotType targetSlotType;
    public int targetSlotIdx;

    public GameObject imageObject;
    public Image targetImage;

    public bool isDragNDrop = false;    

    public int FirstSwapIdx;
    public SlotType firstSlotType;

    public int SecondSwapIdx;
    public SlotType secondSlotType;

    private void Awake()
    {
        //�ʱ� �κ��丮�� 30ĭ���� ����.
        InitializeInventory(30);
        //�ʱ� ��� ���� �޸� ����
        InitializeEquipment(8);
        //�ʱ� ���� ���� �޸� ����
        InitializeWeapon(4);

        //�κ��丮 UI�� Ȯ��
        UpdateInspectorData();
    }

    //ItemTrigger���� ���
    public void UpdateInspectorData()
    {
        for(int i=0; i< inventorySlots.Length; i++)
        {
            inventorySlotData[i].itemCode = inventorySlots[i].itemCode;
            inventorySlotData[i].itemNumber = inventorySlots[i].itemNumber;
            inventorySlotData[i].itemType = inventorySlots[i].itemType.ToString();
        }
    }


    public void InitializeWeapon(int size)
    {
        weaponSlots = new ItemData[size];
        weaponToggle = new bool[size];

        for (int i = 0; i < size; i++)
        {
            GameObject obj = new GameObject("WeaponItem_" + i);
            obj.transform.SetParent(weaponSlotParent, false);

            weaponSlots[i] = obj.AddComponent<ItemData>();

            weaponSlots[i].itemCode = "0000";
            weaponSlots[i].itemNumber = 0;
            weaponSlots[i].itemType = EquipmentType.Null;

            weaponToggle[i] = false;
        }
    }

    // ���â �ʱ� �޸� ����
    public void InitializeEquipment(int size)
    {
        equipmentSlots = new ItemData[size];
        equipmentToggle = new bool[size];

        for (int i=0; i<size; i++)
        {
            GameObject obj = new GameObject("EquipmentItem_" + i);
            obj.transform.SetParent(equipmentSlotParent, false);

            equipmentSlots[i] = obj.AddComponent<ItemData>();

            equipmentSlots[i].itemCode = "0000";
            equipmentSlots[i].itemNumber = 0;
            equipmentSlots[i].itemType = EquipmentType.Null;

            equipmentToggle[i] = false;
        }
    }

    // �κ��丮 ũ�⸦ �����ϴ� �޼���
    public void InitializeInventory(int size)
    {
        inventorySlots = new ItemData[size];
        inventoryToggle = new bool[size];

        inventorySlotData = new InventorySlotData[size];

        for (int i=0; i<size; i++)
        {

            GameObject obj = new GameObject("InventoryItem_" + i);
            obj.transform.SetParent(inventorySlotParent, false);

            inventorySlots[i] = obj.AddComponent<ItemData>();                

            inventorySlots[i].itemCode = "0000";
            inventorySlots[i].itemNumber = 0;
            inventorySlots[i].itemType = EquipmentType.Null;

            inventoryToggle[i] = false;
        }
    }

    //ItemTrigger���� Ʈ���� �ߵ��� ���
    public void AssignItemData(ItemData itemData)
    {
        dropItemData = itemData;
    }

    public int CheckInventory()
    {

        //�κ��丮 â�� ������� ���� üũ
        int blankidx = FindBlank();
        
        if(blankidx == -1)
        {
            int idx = FindIndex(dropItemData);

            //�κ��丮 â�� ��á�µ�, �ش� �������� 9999���ų� �ƿ� ���������� �ʴ� ��Ȳ
            if (idx == -1)
            {
                Debug.Log("�κ��丮�� �� á���ϴ�.");

                return 1;
                
            }
            //�κ��丮�� �̹� �������� �����ϴ� ��Ȳ
            else
            {
                AddItem(dropItemData, idx);

                Debug.Log("�κ��丮(�� ��)�� �������� ����־����ϴ�.");

                return 2;
            }
        }
        else
        {
            int idx = FindIndex(dropItemData);

            //�κ��丮�� �ش� �������� 9999���� �� ���ְų�, �ƿ� �������� �ʴ� ��Ȳ
            if (idx == -1)
            {
                AddItem(dropItemData, blankidx);

                Debug.Log("���ο� ĭ�� �ֽ��ϴ�");

                return 3;

            }
            //�κ��丮�� �̹� �������� �����ϴ� ��Ȳ
            else
            {
                AddItem(dropItemData, idx);

                Debug.Log("�������� ����ֽ��ϴ�.");

                return 4;
            }
        }

       
    }

    public int FindIndex(ItemData itemData)
    {

        for (int i=0; i< inventorySlots.Length; i++)
        {
            //���� �������� �����ϴ��� Ȯ��
            if(itemData.itemCode == inventorySlots[i].itemCode)
            {   
                //���� ������ + ���� ������ ������ ������ 9999���� �ʰ��ϴ��� üũ
                if(inventorySlots[i].itemNumber == 9999) continue;

                return i;
            }
        }

        return -1;
    }

    public int FindBlank()
    {
        for(int i=0; i<inventorySlots.Length; i++)
        {
            if(inventoryToggle[i] is false)
            {
                return i;
            }
        }

        return -1;
    }

    // �κ��丮�� �������� �߰��ϴ� �޼���
    public void AddItem(ItemData itemData, int index)
    {

        // ���� �������� ��Ȳ
        if (inventoryToggle[index] is false)
        {
            inventorySlots[index].itemCode = itemData.itemCode;
            inventoryToggle[index] = true;

            inventorySlots[index].itemType = itemData.itemType;
        }
        
        //���� ������ + ���� ������ ������ ������ 9999�� �̸����� üũ
        if (inventorySlots[index].itemNumber + itemData.itemNumber <= 9999)
        {
            inventorySlots[index].AddItem(itemData.itemNumber);
            itemData.RemoveItem(itemData.itemNumber);
        }
        else
        {
            //�ʰ��ϴ� ���
            int diffecrence = 9999 - inventorySlots[index].itemNumber;

            inventorySlots[index].AddItem(diffecrence);
            itemData.RemoveItem(diffecrence);
        }   
    }

    // �κ��丮���� �������� �����ϴ� �޼���
    public void RemoveItem(ItemData itemData, int index)
    {
        if (index != -1)
        {
            inventorySlots[index].itemCode = "0000";
            inventorySlots[index].itemNumber = 0;
            inventorySlots[index].itemType = EquipmentType.Null;

            inventoryToggle[index] = false;
        }
    }

    public ItemData GetInventoryIndexData(int idx)
    {
        if (idx >= 0 && idx < inventorySlots.Length)
        {
            return inventorySlots[idx];
        }
        else
        {
            Debug.LogError("Index out of range: " + idx);
            return null;
        }
    }

    public ItemData GetEquipmentIndexData(int idx)
    {
        if (idx >= 0 && idx < equipmentSlots.Length)
        {
            return equipmentSlots[idx];
        }
        else
        {
            Debug.LogError("Index out of range: " + idx);
            return null;
        }
    }

    public ItemData GetWeaponIndexData(int idx)
    {
        if (idx >= 0 && idx < weaponSlots.Length)
        {
            return weaponSlots[idx];
        }
        else
        {
            Debug.LogError("Index out of range: " + idx);
            return null;
        }
    }

    public void CheckInstance()
    {
        Debug.Log("�ν��Ͻ� ����");
    }

    public void StartDragEvent()
    {

        isDragNDrop = true;

        FirstSwapIdx = targetSlotIdx;        
        firstSlotType = targetSlotType;

        imageObject.SetActive(true);        

    }
    public void EndDragEvent()
    {
        SecondSwapIdx = targetSlotIdx;
        secondSlotType = targetSlotType;

        if (firstSlotType != SlotType.Null && secondSlotType != SlotType.Null)
        {
            CompareSlotType();
        }

        DeleteDragData();

    }

    public void DeleteDragData()
    {
        FirstSwapIdx = -1;
        firstSlotType = SlotType.Null;
        SecondSwapIdx = -1;
        secondSlotType = SlotType.Null;
        
        targetImage = null;
        imageObject.SetActive(false);

        isDragNDrop = false;
    }

    public void CompareSlotType()
    {
        switch (firstSlotType)
        {
            case SlotType.Inventory:
                
                switch (secondSlotType)
                {
                    case SlotType.Inventory:
                        ItemSwap(inventorySlots[FirstSwapIdx], inventorySlots[SecondSwapIdx]);
                        ItemBoolSwap(inventoryToggle[FirstSwapIdx], inventoryToggle[SecondSwapIdx]);
                        break;
                    case SlotType.Equipment:
                        ItemSwap(inventorySlots[FirstSwapIdx],equipmentSlots[SecondSwapIdx]);
                        ItemBoolSwap(inventoryToggle[FirstSwapIdx],equipmentToggle[SecondSwapIdx]);
                        break;
                    case SlotType.Weapon:
                        ItemSwap(inventorySlots[FirstSwapIdx],weaponSlots[SecondSwapIdx]);
                        ItemBoolSwap(inventoryToggle[FirstSwapIdx],weaponToggle[SecondSwapIdx]);
                        break;
                }
                break;
            case SlotType.Equipment:
                switch (secondSlotType)
                {
                    case SlotType.Inventory:
                        ItemSwap(equipmentSlots[FirstSwapIdx],inventorySlots[SecondSwapIdx]);
                        ItemBoolSwap(equipmentToggle[FirstSwapIdx], inventoryToggle[SecondSwapIdx]);
                        break;
                    case SlotType.Equipment:
                        ItemSwap(equipmentSlots[FirstSwapIdx],equipmentSlots[SecondSwapIdx]);
                        ItemBoolSwap(equipmentToggle[FirstSwapIdx], equipmentToggle[SecondSwapIdx]);
                        break;
                    case SlotType.Weapon:
                        ItemSwap(equipmentSlots[FirstSwapIdx], weaponSlots[SecondSwapIdx]);
                        ItemBoolSwap(equipmentToggle[FirstSwapIdx], weaponToggle[SecondSwapIdx]);
                        break;
                }
                break;
            case SlotType.Weapon:
                switch (secondSlotType)
                {
                    case SlotType.Inventory:
                        ItemSwap(weaponSlots[FirstSwapIdx], inventorySlots[SecondSwapIdx]);
                        ItemBoolSwap(weaponToggle[FirstSwapIdx], inventoryToggle[SecondSwapIdx]);
                        break;
                    case SlotType.Equipment:
                        ItemSwap(weaponSlots[FirstSwapIdx], equipmentSlots[SecondSwapIdx]);
                        ItemBoolSwap(weaponToggle[FirstSwapIdx], equipmentToggle[SecondSwapIdx]);
                        break;
                    case SlotType.Weapon:
                        ItemSwap(weaponSlots[FirstSwapIdx], weaponSlots[SecondSwapIdx]);
                        ItemBoolSwap(weaponToggle[FirstSwapIdx], weaponToggle[SecondSwapIdx]);
                        break;
                }
                break;
        }
    }

    public void ItemSwap(ItemData firstData, ItemData secondData)
    {
        ItemData dummyData;

        dummyData = firstData;
        firstData = secondData;
        secondData = dummyData;

    }

    public void ItemBoolSwap(bool firstBool, bool secondBool)
    {
        bool dummyBool;

        dummyBool = firstBool;
        firstBool = secondBool;
        secondBool = dummyBool;

    }

}
