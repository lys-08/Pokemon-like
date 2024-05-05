using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuButton : MonoBehaviour
{
    private Animator animator_;


    private void Awake()
    {
        animator_ = GetComponent<Animator>();
    }

    public void Clicked()
    {
        animator_.SetTrigger("isPressed");
    }

    public void HoverEnter()
    {
        animator_.SetBool("isSelected", true);
    }
    
    public void HoverExit()
    {
        animator_.SetBool("isSelected", false);
    }
}
