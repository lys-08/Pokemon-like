using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "CaptureItem", menuName = "Inventory/CaptureItem")]
    public class CaptureItemSO : ScriptableObject
    {
        [SerializeField] private ModifierData modifiersData;


        #region IItemAction

        public string ActionName => "Lauch";

        /**
         * TODO : Sound
         */
        // [field: SerializeField] public AudioClip actionSFX {get; private set;};

        public bool Perfom(GameObject pokemon)
        {
            //modifiersData.statModifier.AffectPokemon(pokemon, data.value);

            return true;
        }

        #endregion
    }
}