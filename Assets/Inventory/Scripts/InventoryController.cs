using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // for UiInventoryPage
using Inventory.Model;
using UnityEngine.SocialPlatforms; // for ItemSO


namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage inventoryUi;
        [SerializeField] private InventorySO inventoryData;

        public int inventorySize = 80;

        // TODO : Temporary
        public List<InventoryItem> initialItems = new List<InventoryItem>();


        #region Unity Events Methods

        private void Start()
        {
            // We initialize our inventory
            PrepareUI();
            PrepareInventoryData();
        }


        public void Update()
        {
            /*
             * When the E Key is pressed, we checked if the inventory UI is active or not and
             * we activated or disabled it depending on it's previous state
             */
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventoryUi.isActiveAndEnabled)
                {
                    Time.timeScale = 1f;
                    Cursor.visible = false;
                    inventoryUi.Hide();
                }
                else
                {
                    Time.timeScale = 0f;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    
                    inventoryUi.Show();
                    // We Update our UI
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUi.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
            }
        }

        #endregion

        
        /**
         * Methods that prepares the inventory UI by adding all actions and initializing the UI with the correct size
         */
        private void PrepareUI()
        {
            inventoryUi.InitializeInventoryUI(inventoryData.Size);
            this.inventoryUi.OnDescriptionRequested += HandleDescriptionRequest;
            this.inventoryUi.OnSwapItems += HandleSwapItems;
            this.inventoryUi.OnStartDragging += HandleDragging;
            this.inventoryUi.OnItemActionRequested += HandleItemActionRequest;
        }

        /**
         * Methods that prepare the inventory Data
         */
        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;

            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty) continue;
                inventoryData.AddItem(item);
            }
        }


        #region Actions

        public void HandleDescriptionRequest(int index)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(index);
            if (inventoryItem.IsEmpty)
            {
                inventoryUi.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            inventoryUi.UpdateDescription(index, item.ItemImage, item.Name, item.Description);
        }

        public void HandleSwapItems(int index1, int index2)
        {
            inventoryData.SwapItems(index1, index2);
        }

        public void HandleDragging(int index)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(index);
            if (inventoryItem.IsEmpty) return;
            
            inventoryUi.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        public void HandleItemActionRequest(int index)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(index);
            if (inventoryItem.IsEmpty) return;
            
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                Debug.Log("perform");
                //itemAction.Perfom(pokemon);
            }
            
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(index, 1);
            }
        }

        public void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUi.ResetAllItems();

            foreach (var item in inventoryState)
            {
                inventoryUi.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        #endregion
    }
}
