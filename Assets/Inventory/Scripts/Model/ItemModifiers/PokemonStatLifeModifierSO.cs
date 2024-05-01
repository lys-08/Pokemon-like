using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "LifeModifier", menuName = "Inventory/LifeModifier")]
    public class PokemonStatLifeModifierSO : PokemonStatModifierSO
    {
        public override bool AffectPokemon(PokemonSO pokemonSo, float value, Type type)
        {
            pokemonSo.ko = false;
            pokemonSo.hp = pokemonSo.hpMax;
            return true;
        }
    }
}
