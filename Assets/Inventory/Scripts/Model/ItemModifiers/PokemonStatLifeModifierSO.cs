using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "LifeModifier", menuName = "Inventory/LifeModifier")]
    public class PokemonStatLifeModifierSO : PokemonStatModifierSO
    {
        public override void AffectPokemon(PokemonSO pokemonSo, float value)
        {
            pokemonSo.ko = false;
            pokemonSo.hp = pokemonSo.hpMax;
        }
    }
}
