using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MenuManager
{
    private bool isVisible = false;

    public bool GetIsVisible()
    {
        return isVisible;
    }

    public void OpenMenu()
    {
        isVisible = true;
        gameObject.SetActive(true);
    }
    
    public void Resume()
    {
        isVisible = false;
        gameObject.SetActive(false);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
