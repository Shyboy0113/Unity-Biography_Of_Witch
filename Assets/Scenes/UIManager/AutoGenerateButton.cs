using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGenerateButton : MonoBehaviour
{
    public GameObject buttonPrefab;
    private void Awake()
    {
        int length = MonsterDataManager.Instance.monsterDataList.wrapper_MonsterDataList.Count;

        for (int i=0; i < length; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, transform);
        }

    }
}
