using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;



namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated; // inform that the inventorySO state has changed

        
        /**
         * Methods that initialize the inventory by initializing the list of inventory items
         */
        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        /**
         * Methods that add an item to the list of inventory items
         */
        public void AddItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem{item = item, quantity = quantity};
                    return;
                }
            }
        }

        /**
         * Methods that add an item to the list
         */
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }
        
        /**
         * Methods that returns the item associated with the index passed in parameter
         */
        public InventoryItem GetItemAt(int index)
        {
            return inventoryItems[index];
        }

        /**
         * Methods that swap 2 items knowing their indexes
         */
        internal void SwapItems(int index1, int index2)
        {
            InventoryItem item1 = inventoryItems[index1];
            inventoryItems[index1] = inventoryItems[index2];
            inventoryItems[index2] = item1;
            InformAboutChange();
        }

        /**
         * Methods that inform that the inventory has changed
         */
        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        /**
         * Methods that allow us to know the state of the inventory (the current items in it without the empty ones)
         * -> This allows us to update our inventory without modifying the inventoryItems list
         */
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> dic = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) continue;
                dic[i] = inventoryItems[i];
            }

            return dic;
        }
    }



    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null;
        

        /**
         * Methods that change the quantity of an item and return a new item
         */
        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem { item = this.item, quantity = newQuantity };
        }

        /**
         * Methods that return an empty item
         */
        public static InventoryItem GetEmptyItem() => new InventoryItem { item = null, quantity = 0 };
    }
}