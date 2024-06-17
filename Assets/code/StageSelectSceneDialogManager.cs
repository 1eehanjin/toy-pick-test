using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSceneDialogManager : MonoBehaviour
{
    private GameObject dialog;
    private GameObject dialogBackground;
    private GameObject allClearDialog;
   


    void Awake()
    {
        dialog = GameObject.Find("Dialog");
        dialogBackground = dialog.transform.Find("DialogBackground").gameObject;
        allClearDialog = dialogBackground.transform.Find("AllClearDialog").gameObject;
        
    }


    public void showAllClearDialog()
    {
        allClearDialog.SetActive(true);
        dialogBackground.SetActive(true);
    }



    public void closeAllDialogs()
    {
        allClearDialog.SetActive(false);
        dialogBackground.SetActive(false);
    }



}
