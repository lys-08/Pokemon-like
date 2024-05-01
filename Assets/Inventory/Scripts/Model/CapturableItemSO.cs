using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "CaptureItem", menuName = "Inventory/CaptureItem")]
    public class CaptureItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private ModifierData modifiersData;
        
        #region IItemAction

        public string ActionName => "Launch";
        
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