using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] protected GameObject menuWindow;
    [SerializeField] protected GameObject instructionsWindow;
    [SerializeField] protected GameObject controlsWindow;
    [SerializeField] protected GameObject settingsWindow;


    public void Instructions()
    {
        menuWindow.SetActive(false);
        instructionsWindow.SetActive(true);
    }


    public void Controls()
    {
        menuWindow.SetActive(false);
        controlsWindow.SetActive(true);
    }


    public void Settings()
    {
        menuWindow.SetActive(false);
        settingsWindow.SetActive(true);
    }


    public void Back()
    {
        instructionsWindow.SetActive(false);
        controlsWindow.SetActive(false);
        settingsWindow.SetActive(false);
        menuWindow.SetActive(true);
    }
}
