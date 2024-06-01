using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public int stageNumber;
    
    public Transform stageTransform; // 카메라가 이동할 위치
    


    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        CameraController.Instance.MoveToStage(stageTransform);
        StageSelectManager.OnStageSelected?.Invoke(stageNumber);
        
    }
}