using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/PokemonInventory")]
    public class PokemonInventorySO : ScriptableObject
    {
        [SerializeField] private List<PokemonSO> inventoryItems;
        [field: SerializeField] public int Size { get; set; } = 50;

        public event Action<Dictionary<int, PokemonSO>> OnInventoryUpdated; // inform that the inventorySO state has changed

        
        /**
         * Initialize the inventory by initializing the list of inventory items
         */
        public void Initialize()
        {
            inventoryItems = new List<PokemonSO>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(null); // empty item
            }
        }

        /**
         * Add an item to the list
         */
        public void AddItem(PokemonSO item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i] == null)
                {
                    inventoryItems[i] = item;
                    break;
                }
            }
            
            InformAboutChange();
        }

        /**
         * Remove an item (determine by its index)
         */
        public void RemoveItem(int index)
        {
            if (inventoryItems.Count > index)
            {
                if (inventoryItems[index] == null) return; // If it's already null, we do nothing

                inventoryItems[index] = null;
                InformAboutChange();
            }
        }
        
        /**
         * Returns the item associated with the index passed in parameter
         */
        public PokemonSO GetItemAt(int index)
        {
            return inventoryItems[index];
        }

        /**
         * Swap 2 items knowing their indexes
         */
        public void SwapItems(int index1, int index2)
        {
            PokemonSO item1 = inventoryItems[index1];
            inventoryItems[index1] = inventoryItems[index2];
            inventoryItems[index2] = item1;
            InformAboutChange();
        }

        /**
         * True if the inventory is full, false otherwise
         */
        private bool IsInventoryFull() => !inventoryItems.Where(item => item == null).Any();

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
        public Dictionary<int, PokemonSO> GetCurrentInventoryState()
        {
            Dictionary<int, PokemonSO> dic = new Dictionary<int, PokemonSO>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i] == null) continue;
                dic[i] = inventoryItems[i];
            }

            return dic;
        }
        
        public List<PokemonSO> GetCurrentInventoryState2()
        {
            return inventoryItems;
        }
    }
}