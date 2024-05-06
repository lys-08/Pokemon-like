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
        StartCoroutine(Disappear(menuWindow));
        Appear(instructionsWindow);
    }

    
    public void Controls()
    {
        StartCoroutine(Disappear(menuWindow));
        Appear(controlsWindow);
    }


    public void Settings()
    {
        StartCoroutine(Disappear(menuWindow));
        Appear(settingsWindow);
    }


    public void Back()
    {
        StartCoroutine(Disappear(instructionsWindow));
        StartCoroutine(Disappear(controlsWindow));
        StartCoroutine(Disappear(settingsWindow));
        
        Appear(menuWindow);
    }


    private IEnumerator Disappear(Animator animator)
    {
        if (instructionsWindow.gameObject.activeInHierarchy) yield return null;
        
        animator.SetTrigger("isDisappearing");
        yield return new WaitForSeconds(0.3f);
        animator.gameObject.SetActive(false);
    }
    
    private void Appear(Animator animator)
    {
        animator.gameObject.SetActive(true);
        animator.SetTrigger("isAppearing");
    }
}
