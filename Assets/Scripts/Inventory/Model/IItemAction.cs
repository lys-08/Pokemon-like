using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public interface IItemAction
    {
        public string ActionName { get; }
        // TODO : public AudioClip actionSFX { get; }
        public bool Perform(PokemonSO pokemon);
    }

    [Serializable]
    public class ModifierData
    {
        public PokemonStatModifierSO statModifier;
        public float value;
        public Type type = Type.Simple;
    }

}