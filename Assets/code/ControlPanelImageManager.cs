using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelManager : MonoBehaviour
{
    private GameObject controlPanel;
    private GameObject clawButton;
    private GameObject leftCameraButton;
    private GameObject rightCameraButton;
    private GameObject joystickHandle;
    private GameObject joystickPanel;


    public Sprite[] controlPanelSprites;
    public Sprite[] clawButtonSprites;
    public Sprite[] leftCameraButtonSprites;
    public Sprite[] rightCameraButtonSprites;
    public Sprite[] joystickHandleSprites;
    public Sprite[] joystickPanelSprites;



    void Start()
    {
        controlPanel = GameObject.Find("Control Panel");
        clawButton = GameObject.Find("Claw Button").gameObject;
        leftCameraButton = GameObject.Find("Left Camera Button").gameObject;
        rightCameraButton = GameObject.Find("Right Camera Button").gameObject;
        joystickHandle = GameObject.Find("Handle").gameObject;
        joystickPanel = GameObject.Find("Fixed Joystick").gameObject;
        initControPanel();
    }

    
    public void initControPanel()
    {
        Image controlPanelImage = controlPanel.GetComponent<Image>();
        Image clawButtonImage = clawButton.GetComponent<Image>();
        Image leftCameraButtonImage = leftCameraButton.GetComponent<Image>();
        int currentStageNumber = DataManager.instance.gameData.currentStageNumber;
        controlPanelImage.sprite = controlPanelSprites[currentStageNumber];
        clawButtonImage.sprite = clawButtonSprites[currentStageNumber];
        leftCameraButtonImage.sprite = leftCameraButtonSprites[currentStageNumber];
    }

    


}
