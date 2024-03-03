using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterInformation : MonoBehaviour
{
    public TMP_Text Type;
    public TMP_Text Attack;
    public TMP_Text Defence;
    public TMP_Text Magic_Attack;
    public TMP_Text Magic_Defence;
    public TMP_Text Speed;

    public TMP_Text Description;

    public void ChangeMonsterInformation(int idx)
    {
        MonsterData monsterData = MonsterDataManager.Instance.monsterDataList.wrapper_MonsterDataList[idx];

        Type.text = monsterData.Type;
        Attack.text = monsterData.Attack.ToString();
        Defence.text = monsterData.Defence.ToString();
        Magic_Attack.text = monsterData.Magic_Attack.ToString();
        Magic_Defence.text = monsterData.Magic_Defence.ToString();
        Speed.text = monsterData.Speed.ToString();

        Description.text = monsterData.Description;

    }
    
}
