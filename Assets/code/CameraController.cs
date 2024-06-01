using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public float transitionDuration = 0.3f;

    private Camera mainCamera;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void MoveToStage(Transform targetTransform)
    {
        StartCoroutine(MoveCamera(targetTransform.position));
    }

    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
       
        Vector3 startPosition = mainCamera.transform.position;
        targetPosition.z = startPosition.z;
        targetPosition.x = startPosition.x;
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        float elapsedTime = 0;
        

        while (elapsedTime < transitionDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
    }

    public float dragSpeed = 2.0f;
    private Vector3 dragOrigin;

    // 이동 가능한 경계 값을 설정합니다.
    public float minY = -0.5f;
    public float maxY = 0.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;  // 마우스 클릭 시점의 위치 저장
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(0, pos.y * dragSpeed, 0);

        // 카메라의 현재 위치를 갱신하기 전에 범위 내에 있는지 검사합니다.
        float newY = Mathf.Clamp(transform.position.y - move.y, minY, maxY);

        // 새 위치를 카메라에 적용합니다.
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

