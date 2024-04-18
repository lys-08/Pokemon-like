using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public interface IItemAction
    {
        public string ActionName { get; }
        // public AudioClip actionSFX { get; }
        public bool Perfom(GameObject pokemon);
    }

    [Serializable]
    public class ModifierData
    {
        public PokemonStatModifierSO statModifier;
        public float value;
    }

}