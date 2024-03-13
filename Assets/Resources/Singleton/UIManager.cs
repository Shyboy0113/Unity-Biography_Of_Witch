using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject menuUI;

    [SerializeField] private GameObject itemPickupUIPrefab;
    private Queue<GameObject> uiPool = new Queue<GameObject>();

    private void Awake()
    {
        InitializePool(10);
    }

    private void InitializePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var uiObject = Instantiate(itemPickupUIPrefab);
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

        return uiPool.Dequeue();
    }

    // UI 오브젝트를 풀로 반환하는 메서드
    public void ReturnUIObject(GameObject uiObject)
    {
        uiObject.SetActive(false);
        uiPool.Enqueue(uiObject);
    }

    public void ToggleMenuUI(bool toggle)
    {
        menuUI.SetActive(toggle);    
    }
    
}
