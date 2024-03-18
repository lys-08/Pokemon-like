using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] protected Animator menuWindow = null;
    [SerializeField] protected Animator instructionsWindow = null;
    [SerializeField] protected Animator controlsWindow = null;
    [SerializeField] protected Animator settingsWindow = null;
    

    public void Instructions()
    {
        menuWindow.SetTrigger("isDisappearing");
        instructionsWindow.SetTrigger("isAppearing");
    }


    public void Controls()
    {
        menuWindow.SetTrigger("isDisappearing");
        controlsWindow.SetTrigger("isAppearing");
    }


    public void Settings()
    {
        menuWindow.SetTrigger("isDisappearing");
        settingsWindow.SetTrigger("isAppearing");
    }


    public void Back()
    {
        instructionsWindow.SetTrigger("isDisappearing");
        controlsWindow.SetTrigger("isDisappearing");
        settingsWindow.SetTrigger("isDisappearing");
        menuWindow.SetTrigger("isAppearing");
    }
}
