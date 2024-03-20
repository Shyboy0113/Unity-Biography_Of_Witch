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

    [Header("무기창 데이터")]
    public ItemData[] weaponSlots;
    public bool[] weaponToggle;

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
    public Transform weaponSlotParent;

    //드래그 앤 드롭 관련 변수
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

    //인벤토리 데이터 값 변화시 발생하는 이벤트
    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    private void OnEnable()
    {
        OnInventoryChanged += UpdateInspectorData;
    }

    private void OnDisable()
    {
        OnInventoryChanged -= UpdateInspectorData;
    }

    private void Awake()
    {
        //초기 인벤토리를 30칸으로 지정.
        InitializeInventory(30);
        //초기 장비 슬롯 메모리 설정
        InitializeEquipment(8);
        //초기 무기 슬롯 메모리 설정
        InitializeWeapon(4);

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
        Debug.Log("인스턴스 생성");
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
            OnInventoryChanged?.Invoke();
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
                    case SlotType.Inventory: //인벤토리 두 슬롯간의 교환은 조건문이 필요 없음
                        ItemSwap(ref inventorySlots[FirstSwapIdx], ref inventorySlots[SecondSwapIdx]);
                        ItemBoolSwap(ref inventoryToggle[FirstSwapIdx], ref inventoryToggle[SecondSwapIdx]);
                        break;
                    case SlotType.Equipment:
                        //열거형의 순서와 장비창 배열의 인덱스가 같기 때문에,
                        //인벤토리의 아이템 속성과 두 번째 인덱스가 같으면 통과됨.
                        if ((int)inventorySlots[FirstSwapIdx].itemType == SecondSwapIdx)
                        {
                            ItemSwap(ref inventorySlots[FirstSwapIdx], ref equipmentSlots[SecondSwapIdx]);
                            ItemBoolSwap(ref inventoryToggle[FirstSwapIdx], ref equipmentToggle[SecondSwapIdx]);
                        }
                        break;
                    case SlotType.Weapon:
                        //인벤토리의 아이템 속성이 무기 일 경우
                        if (inventorySlots[FirstSwapIdx].itemType == EquipmentType.Weapon)
                        {                            
                            ItemSwap(ref inventorySlots[FirstSwapIdx], ref weaponSlots[SecondSwapIdx]);
                            ItemBoolSwap(ref inventoryToggle[FirstSwapIdx], ref weaponToggle[SecondSwapIdx]);
                        }
                        break;
                }
                break;

            case SlotType.Equipment:
                switch (secondSlotType)
                {
                    case SlotType.Inventory:
                        //교체하는 인벤토리 아이템의 속성이 위치에 맞는 장비
                        //or 인벤토리창이 비어있을 때(사실상 아이템 장착 해제)
                        if (FirstSwapIdx == (int)inventorySlots[SecondSwapIdx].itemType
                            ||( inventorySlots[SecondSwapIdx].itemCode == "0000" &&  !inventoryToggle[SecondSwapIdx]))
                        {
                            ItemSwap(ref equipmentSlots[FirstSwapIdx], ref inventorySlots[SecondSwapIdx]);
                            ItemBoolSwap(ref equipmentToggle[FirstSwapIdx], ref inventoryToggle[SecondSwapIdx]);
                        }
                        break;
                }
                break;
            case SlotType.Weapon:
                switch (secondSlotType)
                {
                    case SlotType.Inventory:
                        //교체하는 인벤토리 아이템의 속성이 무기,
                        //or 인벤토리창이 비어있을 때(사실상 아이템 장착 해제)
                        if (inventorySlots[SecondSwapIdx].itemType == EquipmentType.Weapon
                             || (inventorySlots[SecondSwapIdx].itemCode == "0000" && !inventoryToggle[SecondSwapIdx]))
                        {
                            ItemSwap(ref weaponSlots[FirstSwapIdx], ref inventorySlots[SecondSwapIdx]);
                            ItemBoolSwap(ref weaponToggle[FirstSwapIdx], ref inventoryToggle[SecondSwapIdx]);
                        }
                        break;
                    case SlotType.Weapon:
                        ItemSwap(ref weaponSlots[FirstSwapIdx], ref weaponSlots[SecondSwapIdx]);
                        ItemBoolSwap(ref weaponToggle[FirstSwapIdx], ref weaponToggle[SecondSwapIdx]);
                        break;
                }
                break;
        }
    }

    public void ItemSwap(ref ItemData firstData, ref ItemData secondData)
    {
        ItemData dummyData;

        dummyData = firstData;
        firstData = secondData;
        secondData = dummyData;

    }

    public void ItemBoolSwap(ref bool firstBool, ref bool secondBool)
    {
        bool dummyBool;

        dummyBool = firstBool;
        firstBool = secondBool;
        secondBool = dummyBool;

    }

}
