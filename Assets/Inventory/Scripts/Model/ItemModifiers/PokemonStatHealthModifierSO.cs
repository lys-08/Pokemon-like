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
            pokemon.GetComponent<PokemonSO>().hp += value;
        }
    }
}
