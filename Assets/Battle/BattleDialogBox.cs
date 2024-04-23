using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [field: SerializeField] public GameObject actions;


    #region Unity Events Methods

    private void Awake()
    {
        ToggleDialogText(true);
        ToggleAction(false);
    }

    #endregion

    /**
     * Update the dialog
     */
    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    /**
     * Corroutine for the progressive type in the box dialog
     */
    public IEnumerator TypeDialog(string dialog)
    {
        Debug.Log("Type Dialog");
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/30);
        }
    }

    /**
     * Enable or disable the dialog box text according to the boolean in parameter
     */
    public void ToggleDialogText(bool val)
    {
        dialogText.enabled = val;
    }
    
    /**
     * Enable or disable the dialog box actions button according to the boolean in parameter
     */
    public void ToggleAction(bool val)
    {
        actions.SetActive(val);
    }
}
