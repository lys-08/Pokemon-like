using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MenuManager
{ 
    public void Resume()
    {
        Cursor.visible = false;
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
