using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // for UiInventoryPage
using Inventory.Model;
using UnityEditor.IMGUI.Controls;
using UnityEngine.SocialPlatforms; // for ItemSO


namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryItemPage itemInventoryUi;
        [SerializeField] private UIInventoryPokemonPage pokemonInventoryUi;
        [SerializeField] private ItemInventorySO itemInventoryData;
        [SerializeField] private PokemonInventorySO pokemonInventoryData;

        private bool itemUiVisible = true;
        
        /***
         * TODO : Sound
         */
        //[SerializeField] private AudioClip dropClip;
        //[SerializeField] private AudioSource audioSource;

        // TODO : Temporary
        public List<InventoryItem> initialItems = new List<InventoryItem>();
        public List<PokemonSO> initialPokemonItems = new List<PokemonSO>();


        #region Unity Events Methods

        private void Awake()
        {
            // We initialize our inventory
            PrepareUI();
            PrepareInventoryData();
        }

        #endregion

        /**
         * Show the inventory
         */
        public void Show()
        {
            itemInventoryUi.gameObject.SetActive(true);
        }
        
        /**
         * Hide the inventory
         */
        public void Hide()
        {
            itemInventoryUi.gameObject.SetActive(false);
        }

        /**
         * Set up the information
         * -> called after the inventoriUI.Show()
         */
        public void SetUpInventoryUI()
        {
            // itemInventoryUI
            foreach (var item in itemInventoryData.GetCurrentInventoryState())
            {
                itemInventoryUi.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
            
            // pokemonInventoryUI
            foreach (var item in pokemonInventoryData.GetCurrentInventoryState())
            {
                pokemonInventoryUi.UpdateData(item.Key, item.Value.image);
            }
        }
        
        /**
         * Prepares the inventory UI by adding all actions and initializing the UI with the correct size
         */
        private void PrepareUI()
        {
            // itemInventoryUI
            itemInventoryUi.InitializeInventoryUI(itemInventoryData.Size);
            this.itemInventoryUi.OnDescriptionRequested += HandleDescriptionRequest;
            this.itemInventoryUi.OnSwapItems += HandleSwapItems;
            this.itemInventoryUi.OnStartDragging += HandleDragging;
            
            // pokemonInventoryUI
            pokemonInventoryUi.InitializeInventoryUI(pokemonInventoryData.Size);
            this.pokemonInventoryUi.OnDescriptionRequested += HandleDescriptionRequest;
            this.pokemonInventoryUi.OnSwapItems += HandleSwapItems;
            this.pokemonInventoryUi.OnStartDragging += HandleDragging;
        }

        /**
         * Prepares the inventory Data
         */
        private void PrepareInventoryData()
        {
            // itemInventoryUI
            itemInventoryData.Initialize();
            itemInventoryData.OnInventoryUpdated += UpdateInventoryUI;

            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty) continue;
                itemInventoryData.AddItem(item);
            }
            
            
            // pokemonInventoryUI
            pokemonInventoryData.Initialize();
            pokemonInventoryData.OnInventoryUpdated += UpdatePokemonInventoryUI;

            foreach (PokemonSO item in initialPokemonItems)
            {
                if (item == null) continue;
                pokemonInventoryData.AddItem(item);
            }
        }


        #region Actions
        
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            itemInventoryUi.ResetAllItems();

            foreach (var item in inventoryState)
            {
                itemInventoryUi.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
        
        private void UpdatePokemonInventoryUI(Dictionary<int, PokemonSO> inventoryState)
        {
            pokemonInventoryUi.ResetAllItems();

            foreach (var item in inventoryState)
            {
                pokemonInventoryUi.UpdateData(item.Key, item.Value.image);
            }
        }

        private void HandleSwapItems(int index1, int index2)
        {
            if (itemUiVisible) itemInventoryData.SwapItems(index1, index2);
            else pokemonInventoryData.SwapItems(index1, index2);
        }

        private void HandleDragging(int index)
        {
            if (itemUiVisible)
            {
                InventoryItem inventoryItem = itemInventoryData.GetItemAt(index);
                if (inventoryItem.IsEmpty) return;
            
                itemInventoryUi.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
            }

            else
            {
                PokemonSO inventoryItem = pokemonInventoryData.GetItemAt(index);
                if (inventoryItem == null) return;
            
                pokemonInventoryUi.CreateDraggedItem(inventoryItem.image);
            }
        }
        
        private void HandleDescriptionRequest(int index)
        {
            if (itemUiVisible)
            {
                InventoryItem inventoryItem = itemInventoryData.GetItemAt(index);
                if (inventoryItem.IsEmpty)
                {
                    itemInventoryUi.ResetSelection();
                    return;
                }

                ItemSO item = inventoryItem.item;
                itemInventoryUi.UpdateDescription(index, item.ItemImage, item.Name, item.Description);
            
                if (inventoryItem.IsEmpty) return;
        
                IItemAction itemAction = inventoryItem.item as IItemAction;
                if (itemAction != null)
                {
                    Debug.Log(item.Name);
                    Debug.Log(itemAction);
                    itemInventoryUi.ShowItemAction(index);
                    itemInventoryUi.AddAction(itemAction.ActionName, () => PerformAction(index));
                }
        
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    itemInventoryUi.AddAction("Drop", () => DropItem(index, 1));
                
                    // TODO : sfx sound
                    // audioSource.PlayOneShot(itemAction.actionSFX);
                
                    //if (inventoryData.GetItemAt(index).IsEmpty) inventoryUi.ResetSelection();
                }
            }

            else
            {
                PokemonSO inventoryItem = pokemonInventoryData.GetItemAt(index);
                if (inventoryItem == null)
                {
                    pokemonInventoryUi.ResetSelection();
                    return;
                }
                pokemonInventoryUi.UpdateDescription(index, inventoryItem);
            
                if (inventoryItem == null) return;
        
                /* TODO
                IItemAction itemAction = inventoryItem as IItemAction;
                if (itemAction != null)
                {
                    pokemonInventoryUi.ShowItemAction(index);
                    pokemonInventoryUi.AddAction(itemAction.ActionName, () => PerformAction(index));
                    Debug.Log("HandleItemActionRequest");
                }*/
        
                IDestroyableItem destroyableItem = inventoryItem as IDestroyableItem;
                if (destroyableItem != null)
                {
                    pokemonInventoryUi.AddAction("Free", () => DropItem(index));
                
                    // TODO : sfx sound
                    // audioSource.PlayOneShot(itemAction.actionSFX);
                
                    //if (inventoryData.GetItemAt(index).IsEmpty) inventoryUi.ResetSelection();
                }
            }
        }

        /**
         * Perform the action of an item
         */
        private void PerformAction(int index)
        {
            if (itemUiVisible)
            {
                InventoryItem inventoryItem = itemInventoryData.GetItemAt(index);
                if (inventoryItem.IsEmpty) return;
            
                IItemAction itemAction = inventoryItem.item as IItemAction;
                if (itemAction != null)
                {
                    Debug.Log("Perform Action");
                    //itemAction.Perfom(pokemon);
                }
            
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    if (inventoryItem.quantity == 1) itemInventoryUi.ResetSelection();
                    itemInventoryData.RemoveItem(index, 1);
                }
            }

            else
            {
                PokemonSO inventoryItem = pokemonInventoryData.GetItemAt(index);
                if (inventoryItem == null) return;
            
                /* TODO
                IItemAction itemAction = inventoryItem as IItemAction;
                if (itemAction != null)
                {
                    Debug.Log("Perform Action");
                    //itemAction.Perfom(pokemon);
                }
            
                IDestroyableItem destroyableItem = inventoryItem as IDestroyableItem;
                if (destroyableItem != null)
                {
                    pokemonInventoryUi.ResetSelection();
                    pokemonInventoryUi.RemoveItem(index);
                }
                */
            }
        }

        /**
         * Drop an item or pokemon that is in the inventory
         */
        private void DropItem(int itemIndex, int quantity = 0)
        {
            if (itemUiVisible)
            {
                itemInventoryData.RemoveItem(itemIndex, quantity);
                if (itemInventoryData.GetItemAt(itemIndex).IsEmpty) itemInventoryUi.ResetSelection();
            
                // TODO : drop audio
                //audioSource.PlayOneShot(dropClip);
            }

            else
            {
                pokemonInventoryData.RemoveItem(itemIndex);
                if (pokemonInventoryData.GetItemAt(itemIndex) == null) pokemonInventoryUi.ResetSelection();
            
                // TODO : drop audio
                //audioSource.PlayOneShot(dropClip);
            }
        }

        #endregion


        #region Button click

        /**
         * Show the item inventory
         */
        public void ShowItemUI()
        {
            itemUiVisible = true;
            itemInventoryUi.Show();
            pokemonInventoryUi.Hide();
        }
        
        /**
         * Show the pokemon inventory
         */
        public void ShowPokemonUI()
        {
            itemUiVisible = false;
            itemInventoryUi.Hide();
            pokemonInventoryUi.Show();
        }

        #endregion
    }
}
