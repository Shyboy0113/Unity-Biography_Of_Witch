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
        if (uiPool.Count == 0) // Ǯ�� ��������� �߰� ����
        {
            InitializePool(1);
        }

        return uiPool.Dequeue();
    }

    // UI ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
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
