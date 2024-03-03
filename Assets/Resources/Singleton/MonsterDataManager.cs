using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class MonsterData
{
    public int Index;
    public string Dev_Name;
    public string Name;
    public string IsBoss;
    public string Type;
    public float Attack;
    public float Defence;
    public float Magic_Attack;
    public float Magic_Defence;
    public float Speed;

    public string Description;

}

[System.Serializable]
public class MonsterDataList
{
    //Json을 활용하기 위해, Wrapper 클래스를 별도로 선언
    //이렇게 해야 Json이 배열을 읽지 못하는 문제를 해결할 수 있음.

    public List<MonsterData> wrapper_MonsterDataList;
}

public class MonsterDataManager : Singleton<MonsterDataManager>
{
    public MonsterDataList monsterDataList;

    public Sprite[] Type_Image;
    public Sprite[] Monster_Image;

    void Awake()
    {
        LoadMonsterData();
    }

    private void Start()
    {
        DisplayMonsterData();
    }
    private void LoadMonsterData()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("MonsterData");
        monsterDataList = JsonUtility.FromJson<MonsterDataList>("{\"wrapper_MonsterDataList\":" + jsonText.text + "}");
    }

    public void DisplayMonsterData()
    {
        foreach (MonsterData monster in monsterDataList.wrapper_MonsterDataList)
        {
            Debug.Log(monster.Index);
            Debug.Log(monster.Dev_Name);
            Debug.Log(monster.Name);
            Debug.Log(monster.IsBoss);
            Debug.Log(monster.Type);
            Debug.Log(monster.Attack);
            Debug.Log(monster.Defence);
            Debug.Log(monster.Magic_Attack);
            Debug.Log(monster.Magic_Defence);
            Debug.Log(monster.Speed);
            Debug.Log(monster.Description);
        }
    }

    public void SetTypeImage(Image typeImage , int idx)
    {
        string typeName = monsterDataList.wrapper_MonsterDataList[idx].Type;

        switch (typeName)
        {
            case "NORMAL":
                typeImage.sprite = Type_Image[1];
                break;
            case "GROUND":
                typeImage.sprite = Type_Image[2];
                break;
            case "FIRE":
                typeImage.sprite = Type_Image[3];
                break;
            case "AQUA":
                typeImage.sprite = Type_Image[4];
                break;
            case "GRASS":
                typeImage.sprite = Type_Image[5];
                break;
            case "ELECTRIC":
                typeImage.sprite = Type_Image[6];
                break;
            case "SPECIAL":
                typeImage.sprite = Type_Image[7];
                break;
            default:
                Debug.Log("오류 발생");
                typeImage.sprite = Type_Image[0];
                break;
        }
    }
}
