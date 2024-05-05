using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/ItemInventory")]
    public class ItemInventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated; // inform that the inventorySO state has changed

        
        /**
         * Initialize the inventory by initializing the list of inventory items
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
         * Add an item to the list of inventory items
         */
        public int AddItem(ItemSO item, int quantity)
        {
            if (!item.IsStackable)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && !IsInventoryFull())
                    {
                        quantity -= AddNonStackableItem(item, 1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        /**
         * Add an item to the list
         */
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        /**
         * Remove nb item (determine by its index)
         */
        public void RemoveItem(int index, int nb)
        {
            if (inventoryItems.Count > index)
            {
                if (inventoryItems[index].IsEmpty) return;

                int amount = inventoryItems[index].quantity - nb;
                if (amount <= 0)
                {
                    inventoryItems[index] = InventoryItem.GetEmptyItem();
                }
                else
                {
                    inventoryItems[index] = inventoryItems[index].ChangeQuantity(amount);
                }
                
                InformAboutChange();
            }
        }

        /**
         * Add an item that wasn't in the inventory
         */
        private int AddNonStackableItem(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem { item = item, quantity = quantity };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }

            return 0;
        }

        /**
         * Add an item that is already in the inventory (by increasing
         * its quantity)
         */
        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                // We check if one of the item in the inventory is the same as the parameter
                if (inventoryItems[i].IsEmpty) continue;
                if (inventoryItems[i].item.ID == item.ID)
                {
                    // The possible amount of items that can be taken
                    int amount = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;
                    
                    // We increment the quantity according to the capacity
                    if (quantity > amount) // the maximum has been reached
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amount;
                        // We doesn't return the quantity in case of having an other item in our inventory
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            // If their is still quantity to add and the inventory has empty places, we add a new item
            while (quantity > 0 && !IsInventoryFull())
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddNonStackableItem(item, newQuantity);
            }

            return quantity;
        }
        
        /**
         * Returns the item associated with the index passed in parameter
         */
        public InventoryItem GetItemAt(int index)
        {
            return inventoryItems[index];
        }

        /**
         * Swap 2 items knowing their indexes
         */
        public void SwapItems(int index1, int index2)
        {
            InventoryItem item1 = inventoryItems[index1];
            inventoryItems[index1] = inventoryItems[index2];
            inventoryItems[index2] = item1;
            InformAboutChange();
        }

        /**
         * True if the inventory is full, false otherwise
         */
        private bool IsInventoryFull() => !inventoryItems.Where(item => item.IsEmpty).Any();

        /**
         * Inform that the inventory has changed
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
         * MChange the quantity of an item and return a new item
         */
        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem { item = this.item, quantity = newQuantity };
        }

        /**
         * Return an empty item
         */
        public static InventoryItem GetEmptyItem() => new InventoryItem { item = null, quantity = 0 };
    }
}