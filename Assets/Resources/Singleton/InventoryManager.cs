using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    Head,
    Top,
    Bottom,
    Glove,
    Shoes,
    Weapon,
    Accessory_1,
    Accessory_2,
    Accessory_3
}

public class InventoryManager : Singleton<InventoryManager>
{
    [System.Serializable]
    public struct InventorySlotData
    {
        public string itemCode;
        public int itemNumber;
    }

    [Header("Ʈ���� ������")]
    //ItemTrigger���� ���޹��� �������� ������
    public ItemData dropItemData;

    [Header("�κ��丮 ������ ����")]
    public InventorySlotData[] inventorySlotData;
    public bool[] inventoryToggle;

    public ItemData[] inventorySlots;

    public Transform structParent;

    private void Awake()
    {
        //�ʱ� �κ��丮�� 30ĭ���� ����.
        InitializeInventory(30);
        UpdateInspectorData();

    }

    public void UpdateInspectorData()
    {
        for(int i=0; i< inventorySlots.Length; i++)
        {
            inventorySlotData[i].itemCode = inventorySlots[i].itemCode;
            inventorySlotData[i].itemNumber = inventorySlots[i].itemNumber;
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
            obj.transform.SetParent(structParent, false);

            inventorySlots[i] = obj.AddComponent<ItemData>();                

            inventorySlots[i].itemCode = "0000";
            inventorySlots[i].itemNumber = 0;

            inventoryToggle[i] = false;
        }
    }

    public void AssignItemData(ItemData itemData)
    {
        dropItemData = itemData;
    }

    public int CheckInventory()
    {

        Debug.Log(dropItemData.itemNumber);

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
            inventoryToggle[index] = false;

        }


    }

}
