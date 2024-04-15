using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem itemPrefab;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private UIInventoryDescription itemDescription;
        [SerializeField] private MouseFollower mouseFollower;

        /*
         * Reference to all item created
         */
        private List<UIInventoryItem> listUiItems = new List<UIInventoryItem>();

        private int currentDragItem_ = -1;

        /**
         * Actions that takes the index of the item for parameter
         */
        public event Action<int> OnDescriptionRequested, // When we left click on our item (border + description)
                                OnItemActionRequested,  // When we right click on our item (show the item actions)
                                OnStartDragging; // To know what to show when an element is dragged
        /**
         * Action for swapping two items => We need both index that are our parameters
         */
        public event Action<int, int> OnSwapItems;


        #region Unity Events Methods

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        #endregion
        

        /**
         * Methods to initialize our inventory with a given size
         */
        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listUiItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;
            }
        }

        /**
         * Methods that update our inventory with all items data we have
         * -> Set the image and quantity of all items
         */
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listUiItems.Count > itemIndex) // We have this item on our list
            {
                listUiItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        /**
         * Methods that reset the dragged item
         */
        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentDragItem_ = -1;
        }

        /**
         * Methods that create/activate and set our mouse follower
         */
        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        /**
         * Methods that reset the selection. This methods reset the current description and selection
         */
        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        /**
         * Methods that deselct all items in the inventory
         */
        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listUiItems)
            {
                item.Deselect();
            }
        }

        /**
         * Methods that reset all of the items
         */
        internal void ResetAllItems()
        {
            foreach (var item in listUiItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        /**
         * Methods that update the description according to the new clicked item (the previous one is deselected)
         */
        internal void UpdateDescription(int index, Sprite image, string name, string description)
        {
            itemDescription.SetDescription(image, name, description);
            DeselectAllItems();
            listUiItems[index].Select();
        }

        /**
         * Methods that show the inventory page
         */
        public void Show()
        {
            gameObject.SetActive(true);
            // To be sure nothing is printed when we have select something in the previous time we had had shown the inventory
            itemDescription.ResetDescription();
            ResetSelection();
        }
        
        /**
         * Methods that hides the inventory page
         */
        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }


        #region Actions

        private void HandleItemSelection(UIInventoryItem obj)
        {
            int index = listUiItems.IndexOf(obj);
            if (index == -1) return;
            
            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleBeginDrag(UIInventoryItem obj)
        {
            int index = listUiItems.IndexOf(obj);
            if (index == -1) return;
            
            currentDragItem_ = index;
            HandleItemSelection(obj);
            OnStartDragging?.Invoke(index);
        }
        
        private void HandleSwap(UIInventoryItem obj)
        {
            int index = listUiItems.IndexOf(obj);
            if (index == -1) return;
            
            OnSwapItems?.Invoke(currentDragItem_, index);
            HandleItemSelection(obj);
        }

        private void HandleEndDrag(UIInventoryItem obj)
        {
            ResetDraggedItem();
        }
        
        private void HandleShowItemActions(UIInventoryItem obj)
        {
            
        }

        #endregion
    }

}