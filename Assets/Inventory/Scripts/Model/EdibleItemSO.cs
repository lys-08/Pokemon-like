using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "EdibleItem", menuName = "Inventory/EdibleItem")]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifiersDatas = new List<ModifierData>();
        
        
        #region IItemAction

        public string ActionName => "Consume";
        
        // public AudioClip actionSFX {get; private set;};
        
        public bool Perfom(GameObject pokemon)
        {
            foreach (ModifierData data in modifiersDatas)
            {
                data.statModifier.AffectPokemon(pokemon, data.value);
            }

            return true;
        }

        #endregion
    }
}

