using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // for UIInventoryItem

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private UIInventoryItem item;

    private Vector2 position_; // Position of our mouse pointer


    private void Awake()
    {
        canvas = gameObject.GetComponentInParent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }

    
    /**
     * Methods that set the data of the drag item that follow the mouse
     */
    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }


    private void Update()
    {
        // 1. Calcul of the position of our mouse pointer
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, 
            Input.mousePosition,
            canvas.worldCamera,
            out position_); // We transform a screen space point to a local space point
        // 2. Transformation of the position in a position in the canvas and affect it
        transform.position = canvas.transform.TransformPoint(position_);
    }

    
    /**
     * Methods that allow us to enable or disable the mouse follower
     */
    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }
}
