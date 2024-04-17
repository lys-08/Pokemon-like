using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;


    #region Unity Events Methods

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            int remainingAmount = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            
            if (remainingAmount == 0) item.DestroyItem();
            else item.Quantity = remainingAmount;
        }
    }

    #endregion
}
