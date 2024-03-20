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
            // �θ� Transform�� �����Ͽ� ������Ʈ�� ����
            var uiObject = Instantiate(itemPickupUIPrefab, poolManagerParent, false);
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

        var uiObject = uiPool.Dequeue();
        // �ʿ��� ��� ���⼭ uiObject�� transform�� �缳���� �� �ֽ��ϴ�.
        return uiObject;
    }

    // UI ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
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
        Debug.Log("UIManager ����");
    }
    
}
