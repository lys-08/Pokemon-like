using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "LifeModifier", menuName = "Inventory/LifeModifier")]
    public class PokemonStatLifeModifierSO : PokemonStatModifierSO
    {
        public override void AffectPokemon(GameObject pokemon, float value)
        {
            PokemonSO pokemonSo = pokemon.GetComponent<PokemonSO>();

            pokemonSo.ko = false;
            pokemonSo.hp = pokemonSo.hpMax;
        }
    }
}
