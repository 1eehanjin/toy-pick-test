using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public Image collectionDollImage;
    public TextMeshProUGUI collectionNameText;
    public TextMeshProUGUI collectionInfoText;

    public Image[] collectionImageRenderers;
    public Sprite[] collectionImages; // 각 스테이지의 UI 이미지를 저장하는 배열

    // 스테이지 클릭 액션 델리게이트 정의
    public static Action<int> OnCollectionSelected;
    public int selectedCollectionNumber = -1;

    public float blinkDuration = 0.5f; // 깜빡이는 주기
    private Coroutine blinkCoroutine;

    void Start()
    {
        UpdateCollectionSprites();
    }

    public void UpdateCollectionSprites()
    {
        for (int i = 0; i < collectionImageRenderers.Length; i++)
        {
            if (DataManager.instance.getData(i) == 1)
            {
                collectionImageRenderers[i].sprite = collectionImages[i];
                RectTransform rt = collectionImageRenderers[i].GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(collectionImages[i].rect.width, collectionImages[i].rect.height);
            }
        }
    }

    private void OnEnable()
    {
        OnCollectionSelected += SelectCollection;
    }

    private void OnDisable()
    {
        OnCollectionSelected -= SelectCollection;
    }

    public void SelectCollection(int stageNumber)
    {
        if (stageNumber == -1 || DataManager.instance.gameData.stageCleared[stageNumber] == 0)
        {
            return;
        }
        selectedCollectionNumber = stageNumber;

        UpdateCollectionInfo();

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        blinkCoroutine = StartCoroutine(Blink(stageNumber));
    }

    private IEnumerator Blink(int collectionNumber)
    {
        while (true)
        {
            // 알파 값을 최소값에서 최대값으로 서서히 변경
            yield return StartCoroutine(FadeTo(collectionNumber, 1f));
            for (int i = 0; i < collectionImageRenderers.Length; i++)
            {
                Color color = collectionImageRenderers[i].color;
                color.a = 1;
                collectionImageRenderers[i].color = color;
            }
            // 알파 값을 최대값에서 최소값으로 서서히 변경
            yield return StartCoroutine(FadeTo(collectionNumber, 0.3f));
        }
    }

    private IEnumerator FadeTo(int collectionNumber, float targetAlpha)
    {
        Color color = collectionImageRenderers[collectionNumber].color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / blinkDuration);
            collectionImageRenderers[collectionNumber].color = color;
            yield return null;
        }

        color.a = targetAlpha;
        collectionImageRenderers[collectionNumber].color = color;
    }

    private void UpdateCollectionInfo()
    {
        var stageData = DataManager.instance.gameData.stageDatas[selectedCollectionNumber];
        collectionNameText.text = stageData.dollName;
        collectionInfoText.text = stageData.dollText;
        collectionDollImage.sprite = collectionImages[selectedCollectionNumber];

        // 새로운 이미지 크기에 맞게 조정
        RectTransform rt = collectionDollImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(collectionImages[selectedCollectionNumber].rect.width, collectionImages[selectedCollectionNumber].rect.height);
    }
}