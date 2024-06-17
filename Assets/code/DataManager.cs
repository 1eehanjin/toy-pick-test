using UnityEngine;

[System.Serializable]
public class GameData
{
    public int[] stageCleared;
    public StageData[] stageDatas;
    public int currentStageNumber;
    public bool alreadyAllCleared;

    public GameData()
    {
        stageCleared = new int[12];
        for (int i = 0; i < stageCleared.Length; i++)
        {
            stageCleared[i] = 0;
        }
    }
 
}

[System.Serializable]
public class StageData
{
    public int machineType;
    public int stageNumber;
    public int settingNumber;
    public int limitTime;
    public int maxAttempts;
    public string stageName;
    public string dollName;
    public string dollText;
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    // public SaveManager saveManager;
    public GameData gameData;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //ResetGame();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //void Start()
    //{
    //    LoadGame();
    //}

    //public void SaveGame()
    //{
    //    saveManager.SaveGame(gameData);
    //}

    //public void LoadGame()
    //{
    //    GameData loadedData = saveManager.LoadGame();
    //    if (loadedData != null)
    //    {
    //        gameData = loadedData;
    //        // 추가로 게임 상태를 업데이트하는 코드 필요 (예: 플레이어 위치 설정)
    //    }
    //}

    public void ResetGame()
    {
        gameData = new GameData();
        // 추가로 게임 상태를 초기화하는 코드 필요
    }

    public int getData(int stageNumber)
    {
        return instance.gameData.stageCleared[stageNumber];
    }

    public void clearStage(int stageNumber)
    {
        instance.gameData.stageCleared[stageNumber] = 1;
    }
}