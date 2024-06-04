using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private GameObject dialog;
    private GameObject dialogBackground;
    private GameObject pauseDialog;
    private GameObject clearDialog;
    private GameObject clearMessageDialog;
    private GameObject startMessageDialog;
    private GameObject failDialog;

    public Sprite[] clearMessageSprites;
    


    void Start()
    {
        dialog = GameObject.Find("Dialog");
        dialogBackground = dialog.transform.Find("DialogBackground").gameObject;
        clearDialog = dialog.transform.Find("ClearDialog").gameObject;
        clearMessageDialog = dialog.transform.Find("ClearMessageDialog").gameObject;
        startMessageDialog = dialog.transform.Find("StartMessageDialog").gameObject;
        failDialog = dialog.transform.Find("FailDialog").gameObject;
        pauseDialog = dialog.transform.Find("PauseDialog").gameObject;
    }

    public void showStartMessageDialog()
    {

        startMessageDialog.SetActive(true);
        dialogBackground.SetActive(true);
    }

    public void showPauseDialog()
    {
        
        pauseDialog.SetActive(true);
        dialogBackground.SetActive(true);
    }

    public void showClearDialog()
    {
        clearDialog.SetActive(true);
        dialogBackground.SetActive(true);
    }

    public void showClearMessageDialog()
    {
        Image imageToChange = clearMessageDialog.transform.GetChild(0).GetComponent<Image>();
        int currentStageNumber = DataManager.instance.gameData.currentStageNumber;
        imageToChange.sprite = clearMessageSprites[currentStageNumber];
        clearMessageDialog.SetActive(true);
        dialogBackground.SetActive(true);
    }

    public void showFailDialog()
    {
        
        failDialog.SetActive(true);
        dialogBackground.SetActive(true);

    }

    public void closeAllDialogs()
    {
        pauseDialog.SetActive(false);
        clearDialog.SetActive(false);
        failDialog.SetActive(false);
        startMessageDialog.SetActive(false);
        clearMessageDialog.SetActive(false);
        dialogBackground.SetActive(false);
    }



}
