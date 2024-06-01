using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table
{
    public int Index { get; set; }
    public string Name { get; set; }
    public string InfoText { get; set;}
    public string Tier { get; set; }
    public string Tribe { get; set; }
}

public class SaveDataManager : MonoBehaviour
{
    
    public void SetTable()
    {
        TextAsset text = Resources.Load<TextAsset>("cats");
        string content = text.text;
        string[] line = content.Split('\n');
        catDicLength = line.Length - 1;
        for (int i = 1; i < line.Length; i++)
        {
            //0번은 설명용
            string[] column = line[i].Split(',');
            int index = 0;
            Table table = new Table();
            table.Index = int.Parse(column[index++]);
            table.Name = column[index++];
            table.InfoText = column[index++];
            table.Tier = column[index++];
            table.Tribe = column[index++];
            catDic.Add(table.Index, table);
        }
    }
    public Table GetTable(int _index)
    {
        if (catDic.ContainsKey(_index))
            return catDic[_index];
        return null;
    }
    private Dictionary<int, Table> catDic = new Dictionary<int, Table>();
    public int catDicLength;
    public int[] holeStatus;

    private int gold = 100000;
    private int catLeaf = 5000;
    private int wood = 5000;
    
    //public static Singletone instance;
    public int[] catSlotStatus;
    public int[] catStatus;
    public buildingData[] buildingDatas;
    public int[] abilityStatus;
    public int[] buildingReward;
    public int numOfBuildingSlots;
    //public int[,] buildingCats = new int[6,4];
    public int sceneNum = 0; //0: 머지 장면 1: 건물 장면
    public bool isBuildingMode = false; //0: 돈벌기 모드 1: 건축 모
    internal string getTable;

    // Start is called before the first frame update
    void Awake()
    {
        //자기 자신의 변수는 awake에서 초기화하고 남의 변수 받아오는건 start에서 하는게 국룰
        //instance = this;
      
        catSlotStatus = new int[9];
        //1: 고양이 있음, 칸 사용 가능 0: 고양이 없음, 칸 사용 가능, -1: 칸 사용 불가, -2: 칸 사용 불가+구매 가느
        //buildingStatus = new int[6];
        //1~ 구매함, 업그레이드 단계 표시 -1 구매 불가 -2 구매가능 미구매
        for (int i = 0; i < 3; i++)
        {
            catSlotStatus[i] = 0;
            
        }
        if (3 < 9)
        {
            catSlotStatus[3] = -2;
            for (int i = 4; i < 9; i++)
            {
                catSlotStatus[i] = -1;
            }
        }
        for (int i = 0; i < numOfBuildingSlots; i++)
        {
            buildingDatas[i].status = 1;
        }
        if (numOfBuildingSlots < 6)
        {
            buildingDatas[numOfBuildingSlots].status = -2;
            for (int i = numOfBuildingSlots + 1; i < 6; i++)
            {
                buildingDatas[i].status = -1;
            }
        }
        SetTable();
        Debug.Log(GetTable(1).InfoText);
    }



    public int getGold()
    {
        return gold;
    }
    public void plusGold(int n)
    {
        gold += n;
    }

    public int getCatLeaf()
    {
        return catLeaf;
    }
    public void plusCatLeaf(int n)
    {
        catLeaf += n;
    }
    public int getWood()
    {
        return wood;
    }
    public void plusWood(int n)
    {
        wood += n;
    }
    public bool minusGold(int n)
    {
        if(gold >= n)
        {
            gold -= n;
            return true;
        }
        return false;
    }
    public bool minusCatLeaf(int n)
    {
        if(catLeaf >= n)
        {
            catLeaf -= n;
            return true;
        }
        return false;
    }
    public bool minusWood(int n)
    {
        if(wood >= n)
        {
            wood -= n;
            return true;
        }
        return false;
    }
}

[System.Serializable]
public struct buildingData
{
    public int status;
    public int[] catStatus;

}
