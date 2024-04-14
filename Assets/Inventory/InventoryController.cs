using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO JULES
 * ON THE PLAYER PREFAB
 * inventoryUi_ = Inventory Prefab in the scene
 * disabled the inventory in the scene (original state = hide)
*/

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UiInventoryPage inventoryUi_;

    public int inventorySize = 10;

    private void Start()
    {
        // We initialize our inventory UI
        inventoryUi_.InitializeInventoryUI(inventorySize);
    }


    public void Update()
    {
        /*
         * When the E Key is pressed, we checked if the inventory UI is active or not and
         * we activated or disabled it depending on it's previous state
         */
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryUi_.isActiveAndEnabled) inventoryUi_.Hide();
            else inventoryUi_.Show();
        }
    }
}
