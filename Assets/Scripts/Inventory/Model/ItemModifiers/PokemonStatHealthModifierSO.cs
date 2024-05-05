using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "HealthModifier", menuName = "Inventory/HealthModifier")]
    public class PokemonStatHealthModifierSO : PokemonStatModifierSO
    {
        public override bool AffectPokemon(PokemonSO pokemonSo, float value, Type type)
        {
            if (pokemonSo.ko) return true;

            float amount = pokemonSo.hp + value;
            if (amount > pokemonSo.hpMax) pokemonSo.hp = pokemonSo.hpMax;
            else pokemonSo.hp += value;
            return true;
        }
    }
}
