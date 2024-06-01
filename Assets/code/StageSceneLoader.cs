using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneLoader : MonoBehaviour
{
    public void LoadStageScene()
    {
        SceneManager.LoadScene(DataManager.instance.gameData.stageDatas[DataManager.instance.gameData.currentStageNumber].machineType.ToString());
    }
}
