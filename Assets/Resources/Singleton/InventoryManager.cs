using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [System.Serializable]
    public struct InventorySlotData
    {
        public string itemCode;
        public int itemNumber;
        public string itemType;
    }



    [Header("장비창 데이터")]
    public ItemData[] equipmentSlots;
    public bool[] equipmentToggle;

    [Header("인벤토리 데이터 정보")]
    public InventorySlotData[] inventorySlotData;

    [Header("인벤토리 데이터")]
    public ItemData[] inventorySlots;
    public bool[] inventoryToggle;

    [Header("트리거 아이템")]
    //ItemTrigger에서 전달받은 아이템의 데이터
    public ItemData dropItemData;

    [Header("인벤토리 GameObject 위치")]
    public Transform inventorySlotParent;
    public Transform equipmentSlotParent;

    private void Awake()
    {
        //초기 인벤토리를 30칸으로 지정.
        InitializeInventory(30);
        //초기 장비창 메모리 설정
        InitializeEquipment(8);

        //인벤토리 UI에 확인
        UpdateInspectorData();
    }

    //ItemTrigger에서 사용
    public void UpdateInspectorData()
    {
        for(int i=0; i< inventorySlots.Length; i++)
        {
            inventorySlotData[i].itemCode = inventorySlots[i].itemCode;
            inventorySlotData[i].itemNumber = inventorySlots[i].itemNumber;
            inventorySlotData[i].itemType = inventorySlots[i].itemType.ToString();
        }
    }

    // 장비창 초기 메모리 설정
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

    // 인벤토리 크기를 지정하는 메서드
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

    //ItemTrigger에서 트리거 발동시 사용
    public void AssignItemData(ItemData itemData)
    {
        dropItemData = itemData;
    }

    public int CheckInventory()
    {

        //인벤토리 창이 비었는지 먼저 체크
        int blankidx = FindBlank();
        
        if(blankidx == -1)
        {
            int idx = FindIndex(dropItemData);

            //인벤토리 창이 꽉찼는데, 해당 아이템이 9999개거나 아예 존재하지도 않는 상황
            if (idx == -1)
            {
                Debug.Log("인벤토리가 꽉 찼습니다.");

                return 1;
                
            }
            //인벤토리에 이미 아이템이 존재하는 상황
            else
            {
                AddItem(dropItemData, idx);

                Debug.Log("인벤토리(꽉 참)에 아이템을 집어넣었습니다.");

                return 2;
            }
        }
        else
        {
            int idx = FindIndex(dropItemData);

            //인벤토리에 해당 아이템이 9999개로 꽉 차있거나, 아예 존재하지 않는 상황
            if (idx == -1)
            {
                AddItem(dropItemData, blankidx);

                Debug.Log("새로운 칸에 넣습니다");

                return 3;

            }
            //인벤토리에 이미 아이템이 존재하는 상황
            else
            {
                AddItem(dropItemData, idx);

                Debug.Log("아이템을 집어넣습니다.");

                return 4;
            }
        }

       
    }

    public int FindIndex(ItemData itemData)
    {

        for (int i=0; i< inventorySlots.Length; i++)
        {
            //같은 아이템이 존재하는지 확인
            if(itemData.itemCode == inventorySlots[i].itemCode)
            {   
                //기존 아이템 + 새로 들어오는 아이템 개수가 9999개를 초과하는지 체크
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

    // 인벤토리에 아이템을 추가하는 메서드
    public void AddItem(ItemData itemData, int index)
    {

        // 더미 데이터인 상황
        if (inventoryToggle[index] is false)
        {
            inventorySlots[index].itemCode = itemData.itemCode;
            inventoryToggle[index] = true;

            inventorySlots[index].itemType = itemData.itemType;
        }
        
        //기존 아이템 + 새로 들어오는 아이템 개수가 9999개 미만인지 체크
        if (inventorySlots[index].itemNumber + itemData.itemNumber <= 9999)
        {
            inventorySlots[index].AddItem(itemData.itemNumber);
            itemData.RemoveItem(itemData.itemNumber);
        }
        else
        {
            //초과하는 경우
            int diffecrence = 9999 - inventorySlots[index].itemNumber;

            inventorySlots[index].AddItem(diffecrence);
            itemData.RemoveItem(diffecrence);
        }   
    }

    // 인벤토리에서 아이템을 제거하는 메서드
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

    public ItemData GetIndexData(int idx)
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

    public void CheckInstance()
    {
        Debug.Log("인스턴스 생성");
    }
    

}
