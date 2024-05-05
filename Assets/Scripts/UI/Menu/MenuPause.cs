using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MenuManager
{ 
    [SerializeField] private Animator inGameWindow;


    public void Pause()
    {
        Cursor.visible = true;
        inGameWindow.SetTrigger("isDisappearing");
        Time.timeScale = 0;
        menuWindow.SetTrigger("isAppearing");
    }


    public void Resume()
    {
        Cursor.visible = false;
        inGameWindow.SetTrigger("isAppearing");
        Time.timeScale = 1;
        menuWindow.SetTrigger("isDisappearing");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
