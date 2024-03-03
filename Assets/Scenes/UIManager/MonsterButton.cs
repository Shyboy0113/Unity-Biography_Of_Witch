using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterButton : MonoBehaviour
{
    private MonsterInformation _monsterInformation;

    public TMP_Text monsterIndex;
    public TMP_Text monsterName;

    public Image typeImage;

    private int idx;

    private void Awake()
    {        
        idx = transform.GetSiblingIndex();
           
        _monsterInformation = GameObject.Find("MonsterInformation").GetComponent<MonsterInformation>();

        MonsterData monsterData = MonsterDataManager.Instance.monsterDataList.wrapper_MonsterDataList[idx];

        monsterIndex.text = "No. " + monsterData.Index.ToString();
        monsterName.text = monsterData.Name;

        MonsterDataManager.Instance.SetTypeImage(typeImage,idx);

    }
    public void GetMonsterInfromation()
    {        
        if (_monsterInformation is not null)
        {
            _monsterInformation.ChangeMonsterInformation(idx);
        }

    }

}
