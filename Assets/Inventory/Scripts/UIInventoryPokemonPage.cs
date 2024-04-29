using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPokemonPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryPokemonItem itemPrefab;
        [SerializeField] private GameObject content;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private ItemActionPanel actionPanel;
        [SerializeField] private UIInventoryDescription itemDescription;
        [SerializeField] private PokemonMouseFollower mouseFollower;

        /*
         * Reference to all item created
         */
        private List<UIInventoryPokemonItem> listPokemonItems = new List<UIInventoryPokemonItem>();

        private int currentDragItem_ = -1;

        /**
         * Actions that takes the index of the item for parameter
         */
        public event Action<int> OnDescriptionRequested, // When we left click on our item (border + description)
                                OnStartDragging; // To know what to show when an element is dragged
        /**
         * Action for swapping two items => We need both index that are our parameters
         */
        public event Action<int, int> OnSwapItems;


        #region Unity Events Methods

        private void Awake()
        {
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
            Hide();
        }

        #endregion
        

        /**
         * Initialize our inventory with a given size
         */
        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryPokemonItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentPanel);
                uiItem.transform.localScale = Vector3.one;
                listPokemonItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
            }
        }

        /**
         * Update our inventory with all items data we have
         * -> Set the image of all items
         */
        public void UpdateData(int itemIndex, Sprite itemImage)
        {
            if (listPokemonItems.Count > itemIndex) // We have this item on our list
            {
                listPokemonItems[itemIndex].SetData(itemImage);
            }
        }

        /**
         * Reset the dragged item
         */
        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentDragItem_ = -1;
        }

        /**
         * Create/activate and set our mouse follower
         */
        public void CreateDraggedItem(Sprite sprite)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite);
        }

        /**
         * Add an action button to the action panel
         */
        public void AddAction(string actionName, System.Action performAction)
        {
            actionPanel.AddButton(actionName, performAction);
        }
        
        /**
         * Show the action panel that contains all action buttons associated with the item
         */
        public void ShowItemAction(int index)
        {
            actionPanel.Toggle(true);
        }

        /**
         * Reset the selection. This methods reset the current description and selection
         */
        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        /**
         * Deselect all items in the inventory
         */
        private void DeselectAllItems()
        {
            foreach (UIInventoryPokemonItem item in listPokemonItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }

        /**
         * Reset all of the items
         */
        public void ResetAllItems()
        {
            foreach (var item in listPokemonItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        /**
         * Update the description according to the new clicked item (the previous one is deselected)
         */
        public void UpdateDescription(int index, PokemonSO pokemon)
        {
            itemDescription.SetPokemonDescription(pokemon);
            DeselectAllItems();
            listPokemonItems[index].Select();
        }

        /**
         * Show the inventory page
         */
        public void Show()
        {
            content.SetActive(true);
            // To be sure nothing is printed when we have select something in the previous time we had had shown the inventory
            ResetSelection();
        }
        
        /**
         * Hides the inventory page
         */
        public void Hide()
        {
            actionPanel.Toggle(false);
            content.SetActive(false);
            ResetDraggedItem();
        }


        #region Actions

        private void HandleItemSelection(UIInventoryPokemonItem obj)
        {
            int index = listPokemonItems.IndexOf(obj);
            if (index == -1) return;
            
            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleBeginDrag(UIInventoryPokemonItem obj)
        {
            int index = listPokemonItems.IndexOf(obj);
            if (index == -1) return;
            
            currentDragItem_ = index;
            HandleItemSelection(obj);
            OnStartDragging?.Invoke(index);
        }
        
        private void HandleSwap(UIInventoryPokemonItem obj)
        {
            int index = listPokemonItems.IndexOf(obj);
            if (index == -1) return;
            
            OnSwapItems?.Invoke(currentDragItem_, index);
            HandleItemSelection(obj);
        }

        private void HandleEndDrag(UIInventoryPokemonItem obj)
        {
            ResetDraggedItem();
        }

        #endregion
    }

}