using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;


public class StageManager : MonoBehaviour
{
    public static Action onClear;
    public static Action onGameOver;
    public static Action onPause;
    public static Action onResume;
    public static Action onUseAttempt;

    public GameObject[] dollSettings;
    public GameObject[] machineParts;
    public Material[] skyboxMaterials;
    public int currentStageNumber;
    public StageData currentStageData;
    public float remainingTime;
    
    public int remainingAttempts;
    private bool isStageActive = false;
    private DialogManager dialogManager;

    // UI를 업데이트하기 위한 TextMeshPro 텍스트 오브젝트
    public TextMeshProUGUI attemptsText;
    public TextMeshProUGUI timeText;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusicManager.instance.ChangeBackgroundMusic2();
        InitStage();
        GameObject sceneManagers = GameObject.Find("SceneManagers");
        dialogManager = sceneManagers.GetComponent<DialogManager>();
        onClear += ClearStage;
        onGameOver += GameOverStage;
        onPause += PauseGame;
        onResume += ResumeGame;
        onUseAttempt += UseAttempt;

        ShowStartMessage();
    }

    private void OnDestroy()
    {
        onClear -= ClearStage;
        onGameOver -= GameOverStage;
        onPause -= PauseGame;
        onResume -= ResumeGame;
        onUseAttempt -= UseAttempt;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (isStageActive)
        {
            UpdateTimer();
        }
    }

    // 스테이지 초기화
    public void InitStage()
    {
        currentStageNumber = DataManager.instance.gameData.currentStageNumber;
        currentStageData = DataManager.instance.gameData.stageDatas[currentStageNumber];
        remainingTime = currentStageData.limitTime;
        remainingAttempts = currentStageData.maxAttempts;
        UpdateUI();
        ChangeSkybox();
        isStageActive = true;
        machineParts[currentStageData.settingNumber].SetActive(true);
        dollSettings[currentStageData.settingNumber].SetActive(true);
        Time.timeScale = 1;
    }

    public void ChangeSkybox()
    {
        RenderSettings.skybox = skyboxMaterials[currentStageData.settingNumber];
        DynamicGI.UpdateEnvironment(); // 환경 조명을 업데이트합니다.
    }


public void UseAttempt()
{
    if (remainingAttempts > 0)
    {
        remainingAttempts--;
        UpdateUI();

        if (remainingAttempts == 0)
        {
            WaitAndGameOver();
        }
    }
}

private async void WaitAndGameOver()
{
    await Task.Delay(10000); // 5000 milliseconds = 5 seconds
    GameOverStage();
}

// 타이머 업데이트
private void UpdateTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            
            UpdateUI();

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                GameOverStage();
            }
        }
    }

    
  

    // 스테이지 종료
    private void GameOverStage()
    {
        isStageActive = false;
        dialogManager.showFailDialog();
    }

    public void ClearStage()
    {
        DataManager.instance.clearStage(currentStageNumber);
        isStageActive = false;
        dialogManager.showClearMessageDialog();
    }

    // 게임 일시정지
    public void PauseGame()
    {
        Time.timeScale = 0;
        dialogManager.showPauseDialog();
        isStageActive = false;
    }

    // 게임 일시정지
    public void ShowStartMessage()
    {
        Time.timeScale = 0;
        dialogManager.showStartMessageDialog();
        isStageActive = false;
    }

    // 게임 재개
    public void ResumeGame()
    {
        Time.timeScale = 1;
        dialogManager.closeAllDialogs();
        isStageActive = true;
    }

    // UI 업데이트
    private void UpdateUI()
    {
        if (attemptsText != null)
        {
            attemptsText.text = $"{remainingAttempts}";
        }

        if (timeText != null)
        {
            timeText.text = $"{remainingTime:F2}"; // 소수점 두 자리까지 표시
        }

        if (slider != null)
        {
            slider.value = remainingTime / currentStageData.limitTime;
        }
    }
}