using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "ConsumableItem", menuName = "Inventory/ConsumableItem")]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private ModifierData modifiersData;
        
        
        #region IItemAction

        public string ActionName => "Consume";
        
        /**
         * TODO : Sound
         */
        // [field: SerializeField] public AudioClip actionSFX {get; private set;};
        
        public bool Perform(PokemonSO pokemon)
        {
            modifiersData.statModifier.AffectPokemon(pokemon, modifiersData.value);

            return true;
        }

        #endregion
    }
}

