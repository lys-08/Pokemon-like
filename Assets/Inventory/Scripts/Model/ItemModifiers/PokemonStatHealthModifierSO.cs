using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "HealthModifier", menuName = "Inventory/HealthModifier")]
    public class PokemonStatHealthModifierSO : PokemonStatModifierSO
    {
        public override void AffectPokemon(GameObject pokemon, float value)
        {
            PokemonSO pokemonSo = pokemon.GetComponent<PokemonSO>();
            if (pokemonSo.ko) return;

            float amount = pokemonSo.hp + value;
            if (amount > pokemonSo.hpMax) pokemonSo.hp = pokemonSo.hpMax;
            else pokemonSo.hp += value;
        }
    }
}
