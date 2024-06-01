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
    private GameObject failDialog;
    


    void Start()
    {
        dialog = GameObject.Find("Dialog");
        dialogBackground = dialog.transform.Find("DialogBackground").gameObject;
        clearDialog = dialog.transform.Find("ClearDialog").gameObject;
        failDialog = dialog.transform.Find("FailDialog").gameObject;
        pauseDialog = dialog.transform.Find("PauseDialog").gameObject;
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
        dialogBackground.SetActive(false);
    }



}
