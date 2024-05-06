using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
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

        [SerializeField] private GameObject pokemonInventoryButton;

        private bool itemUiVisible = true;
        private int mainPokemonIndex = 0;
        public Action<int> OnPerformActionInBattle;
        
        /***
         * TODO : Sound
         */
        [SerializeField] private AudioSource dropSound;
        [SerializeField] private AudioSource click;
        [SerializeField] private AudioSource freeSound;
        [SerializeField] private AudioSource makePrimarySound;

        // TODO : Temporary
        public List<InventoryItem> initialItems = new List<InventoryItem>();
        public List<PokemonSO> initialPokemonItems = new List<PokemonSO>();


        #region Unity Events Methods

        private void Awake()
        {
            // We initialize our inventory
            PrepareUI();
            PrepareInventoryData();
            
            SetMainPokemon(mainPokemonIndex);
        }

        #endregion


        #region Getters

        public UIInventoryItemPage GetInventoryItem()
        {
            return itemInventoryUi;
        }
        
        public ItemInventorySO GetInventoryItemData()
        {
            return itemInventoryData;
        }
        
        public UIInventoryPokemonPage GetInventoryPokemon()
        {
            return pokemonInventoryUi;
        }
        
        public PokemonInventorySO GetInventoryPokemonData()
        {
            return pokemonInventoryData;
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
            ShowItemUI();
            itemInventoryUi.gameObject.SetActive(false);
        }
        
        /**
         * Update the main pokemon if the current one is ko
         */
        public void UpdateMainPokemon()
        {
            if (pokemonInventoryData.GetItemAt(mainPokemonIndex) == null || pokemonInventoryData.GetItemAt(mainPokemonIndex).ko)
            {
                int i = 0;
                foreach (var item in pokemonInventoryData.GetCurrentInventoryState())
                {
                    if (item.Value == null) continue;
                    if (!item.Value.ko)
                    {
                        SetMainPokemon(item.Key);
                        break;
                    }

                    i++;
                }
            }
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

        /**
         * Returns the main pokemon
         */
        public PokemonSO GetMainPokemon()
        {
            pokemonInventoryData.GetCurrentInventoryState().TryGetValue(mainPokemonIndex, out PokemonSO mainPokemon);
            return mainPokemon;
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
                itemInventoryUi.ShowItemAction(index);
        
                EdibleItemSO itemAction = inventoryItem.item as EdibleItemSO;
                if (itemAction != null)
                {
                    itemInventoryUi.AddAction(itemAction.ActionName, () => PerformAction(index));
                }
        
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    itemInventoryUi.AddAction("Drop", () => DropItem(index, 1));
                
                    // DONE : sfx sound
                    click.Play();
                
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
                itemInventoryUi.ShowItemAction(index);
        
                /*
                 * 1. Action that allows the player to choose the pokemon as the main one
                 * 2. Action that allows the player to free a pokemon
                 *
                 * TODO : SFX sound
                 */
                 click.Play();
                pokemonInventoryUi.AddAction("Main Pokemon", () => SetMainPokemon(index));
                pokemonInventoryUi.AddAction("Free", () => DropItem(index));
            }
        }
        
        private void HandleDescriptionRequestInBattle(int index)
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
            itemInventoryUi.ShowItemAction(index);
    
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemInventoryUi.AddAction(itemAction.ActionName, () => PerformActionInBattle(index));
            }
        }

        /**
         * Activate the action of the pokeball
         */
        public void ActionBattle(bool val)
        {
            if (val)
            {
                Show();
                pokemonInventoryButton.SetActive(false);
                this.itemInventoryUi.OnDescriptionRequested -= HandleDescriptionRequest;
                this.itemInventoryUi.OnDescriptionRequested += HandleDescriptionRequestInBattle;
            }
            else
            {
                pokemonInventoryButton.SetActive(true);
                Hide();
                this.itemInventoryUi.OnDescriptionRequested -= HandleDescriptionRequestInBattle;
                this.itemInventoryUi.OnDescriptionRequested += HandleDescriptionRequest;
            }
        }

        /**
         * Perform the action of an item
         */
        private void PerformAction(int index)
        {
            InventoryItem inventoryItem = itemInventoryData.GetItemAt(index);
            if (inventoryItem.IsEmpty) return;
        
            EdibleItemSO edibleItemAction = inventoryItem.item as EdibleItemSO;
            if (edibleItemAction != null)
            {
                edibleItemAction.Perform(pokemonInventoryData.GetItemAt(mainPokemonIndex));
            }
        
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                if (inventoryItem.quantity == 1) itemInventoryUi.ResetSelection();
                itemInventoryData.RemoveItem(index, 1);
            }
        }
        
        /**
         * Perform the action of an item in battle and returns the name of the object use
         */
        private void PerformActionInBattle(int index)
        {
            OnPerformActionInBattle?.Invoke(index);
        }

        /**
         * Add a pokemon to the collection
         * -> used in battle
         */
        public void AddPokemonToCollection(PokemonSO pokemon)
        {
            pokemonInventoryData.AddItem(pokemon);
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
            
                // DONE : drop audio
                dropSound.Play();
            }

            else
            {
                pokemonInventoryData.RemoveItem(itemIndex);
                if (pokemonInventoryData.GetItemAt(itemIndex) == null) pokemonInventoryUi.ResetSelection();
                UpdateMainPokemon();
            
                // DONE : drop audio
                freeSound.Play();
            }
        }
        
        /**
         * Set the main pokemon
         */
        private void SetMainPokemon(int index)
        {
            pokemonInventoryUi.UpdateMainPokemon(mainPokemonIndex, index);
            mainPokemonIndex = index;

            makePrimarySound.Play();
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
