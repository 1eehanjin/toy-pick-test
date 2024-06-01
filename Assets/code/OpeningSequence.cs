using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class SceneSetting
{
    public Sprite backgroundImage;
    public string storyText;
    public bool enablePan;
    
    public float panSpeed;
    
    public float cameraZoomTime; // 카메라 줌인 시간
    public Vector3 initialBackgroundPosition; // 배경 이미지 초기 위치
    public float maxPanDistance; // pan의 최대 이동 거리
}

public class OpeningSequence : MonoBehaviour
{
    [Header("UI Elements")]
    public Image backgroundImage;
    public TextMeshProUGUI storyText;

    [Header("Scene Settings")]
    public SceneSetting[] sceneSettings; // 각 장면별 설정 배열

    private int currentIndex = 0;
    private Camera mainCamera;
    private bool isTransitioning = false; // 상태 변수 추가

    void Start()
    {
        mainCamera = Camera.main;
        if (sceneSettings.Length > 0)
        {
            ApplySceneSetting(sceneSettings[0]);
        }
    }

    void Update()
    {
        // 터치 또는 마우스 클릭 감지
        if (!isTransitioning && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            AdvanceToNext();
        }

        // 배경 이미지 이동
        if (sceneSettings[currentIndex].enablePan)
        {
            float distance = (backgroundImage.rectTransform.localPosition - sceneSettings[currentIndex].initialBackgroundPosition).magnitude;
            if (distance < sceneSettings[currentIndex].maxPanDistance)
            {
                backgroundImage.rectTransform.localPosition += Vector3.right * sceneSettings[currentIndex].panSpeed * Time.deltaTime;
            }
        }
    }

    void AdvanceToNext()
    {
        isTransitioning = true; // 상태 변경

        currentIndex++;
        if (currentIndex >= sceneSettings.Length)
        {
            SceneManager.LoadScene("StageSelectScene"); // "NextScene"은 전환할 씬의 이름입니다.
            return;
        }

        ApplySceneSetting(sceneSettings[currentIndex]);

        // Transition을 끝낸 후 상태를 원래대로 돌려놓기
        StartCoroutine(EndTransition());
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(1f); // 임의의 딜레이 설정 (0.5초)
        isTransitioning = false;
    }

    void ApplySceneSetting(SceneSetting setting)
    {
        backgroundImage.sprite = setting.backgroundImage;
        storyText.text = setting.storyText;

        // 배경 이미지 위치 초기화
        backgroundImage.rectTransform.localPosition = setting.initialBackgroundPosition;
    }
}