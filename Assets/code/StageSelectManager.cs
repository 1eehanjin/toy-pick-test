using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class StageSelectManager : MonoBehaviour
{
    
    private void Awake()
    {
        
    }

    // 기존 필드 및 메서드
    
    
    private TextMeshProUGUI stageInfoText;

    public string stageInfoTextObjectName = "Text (TMP)";

    public SpriteRenderer[] stageSpriteRenderers;
    public Sprite[] clearedSprites; // 각 스테이지의 UI 이미지를 저장하는 배열

    // 스테이지 클릭 액션 델리게이트 정의
    public static Action<int> OnStageCleared;
    public static Action<int> OnStageSelected;


    public float blinkDuration = 0.5f; // 깜빡이는 주기

    private Coroutine blinkCoroutine;

    void Start()
    {
        UpdateStageSprites();
        BackgroundMusicManager.instance.ChangeBackgroundMusic1();

        // Text (TMP) 오브젝트에서 TextMeshProUGUI 컴포넌트 가져오기
        GameObject textObject = GameObject.Find(stageInfoTextObjectName);
        if (textObject != null)
        {
            stageInfoText = textObject.GetComponent<TextMeshProUGUI>();
            if (stageInfoText == null)
            {
                Debug.LogError("TextMeshProUGUI 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError($"'{stageInfoTextObjectName}' 이름을 가진 오브젝트를 찾을 수 없습니다.");
        }
        GameObject sceneManagers = GameObject.Find("SceneManagers");
        StageSelectSceneDialogManager dialogManager= sceneManagers.GetComponent<StageSelectSceneDialogManager>();
        if (!DataManager.instance.gameData.alreadyAllCleared)
        {
            bool allClear = true;
            for (int i = 0; i < stageSpriteRenderers.Length; i++)
            {
                if (DataManager.instance.getData(i) == 0)
                {
                    allClear = false;
                }
            }
            if (allClear)
            {
                dialogManager.showAllClearDialog();
                DataManager.instance.gameData.alreadyAllCleared = true;
            }
        }
            
    }

    public void UpdateStageSprites()
    {
        for (int i = 0; i < stageSpriteRenderers.Length; i++)
        {
            if (DataManager.instance.getData(i) == 1)
            {
                stageSpriteRenderers[i].sprite = clearedSprites[i];
            }
        }
    }

    // 특정 스테이지를 클리어 처리하는 메서드
    public void ClearStage(int stageNumber)
    {
        DataManager.instance.clearStage(stageNumber);
        UpdateStageSprites();
    }

    private void OnEnable()
    {
        // 스테이지 선택 액션에 리스너 추가
        OnStageCleared += ClearStage;
        OnStageSelected += SelectStage;
    }

    private void OnDisable()
    {
        // 스테이지 선택 액션에서 리스너 제거
        OnStageCleared -= ClearStage;
        OnStageSelected -= SelectStage;
    }

   

    public void SelectStage(int stageNumber)
    {
        DataManager.instance.gameData.currentStageNumber = stageNumber;
        
        UpdateStageInfoText();

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        
        blinkCoroutine = StartCoroutine(Blink(stageNumber));
        
    }

    private IEnumerator Blink(int stageNumber)
    {
        while (true)
        {
            // 알파 값을 최소값에서 최대값으로 서서히 변경
            yield return StartCoroutine(FadeTo(stageNumber, 1f));
            for (int i = 0; i < stageSpriteRenderers.Length; i++)
            {
                Color color = stageSpriteRenderers[i].color;
                color.a = 1;
                stageSpriteRenderers[i].color = color;
            }
            // 알파 값을 최대값에서 최소값으로 서서히 변경
            yield return StartCoroutine(FadeTo(stageNumber, 0.3f));
        }
    }

    private IEnumerator FadeTo(int stageNumber, float targetAlpha)
    {
        Color color = stageSpriteRenderers[stageNumber].color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / blinkDuration);
            stageSpriteRenderers[stageNumber].color = color;
            yield return null;
        }

        color.a = targetAlpha;
        stageSpriteRenderers[stageNumber].color = color;
    }

    private void UpdateStageInfoText()
    {
        stageInfoText.text = DataManager.instance.gameData.currentStageNumber.ToString("D2") + " " + DataManager.instance.gameData.stageDatas[DataManager.instance.gameData.currentStageNumber].stageName;
    }

    public void LoadStageScene()
    {
        SceneManager.LoadScene("GameScene"+DataManager.instance.gameData.stageDatas[DataManager.instance.gameData.currentStageNumber].machineType.ToString());
    }
}