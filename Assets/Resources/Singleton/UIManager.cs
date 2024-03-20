using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject menuUI;

    public bool isMenu = false;

    [SerializeField] private GameObject itemPickupUIPrefab;
    [SerializeField] private Transform poolManagerParent;

    private Queue<GameObject> uiPool = new Queue<GameObject>();

    private void Awake()
    {
        InitializePool(10);
    }

    private void InitializePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // 부모 Transform을 설정하여 오브젝트를 생성
            var uiObject = Instantiate(itemPickupUIPrefab, poolManagerParent, false);
            uiObject.SetActive(false);
            uiPool.Enqueue(uiObject);
        }
    }


    public GameObject GetUIObject()
    {
        if (uiPool.Count == 0) // 풀이 비어있으면 추가 생성
        {
            InitializePool(1);
        }

        var uiObject = uiPool.Dequeue();
        // 필요한 경우 여기서 uiObject의 transform을 재설정할 수 있습니다.
        return uiObject;
    }

    // UI 오브젝트를 풀로 반환하는 메서드
    public void ReturnUIObject(GameObject uiObject)
    {
        uiObject.SetActive(false);
        uiObject.transform.SetParent(poolManagerParent);
        uiPool.Enqueue(uiObject);
    }

    public void ToggleMenuUI(bool toggle)
    {
        menuUI.SetActive(toggle);
    }

    public void InstanceManager()
    {
        Debug.Log("UIManager 생성");
    }
    
}
