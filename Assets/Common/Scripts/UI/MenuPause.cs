using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MenuManager
{ 
    [SerializeField] private GameObject inGameWindow;


    public void Pause()
    {
        Cursor.visible = true;
        inGameWindow.SetActive(false);
        Time.timeScale = 0;
        menuWindow.SetActive(true);
    }


    public void Resume()
    {
        Cursor.visible = false;
        inGameWindow.SetActive(true);
        Time.timeScale = 1;
        menuWindow.SetActive(false);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
