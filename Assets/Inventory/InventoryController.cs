using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UiInventoryPage inventoryUi_;

    public int inventorySize = 10;

    private void Start()
    {
        inventoryUi_.InitializeInventoryUI(inventorySize);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(inventoryUi_.isActiveAndEnabled) inventoryUi_.Hide();
            else inventoryUi_.Show();
        }
    }
}
