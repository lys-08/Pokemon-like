using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PokemonStatModifierSO : ScriptableObject
{
    public abstract void AffectPokemon(PokemonSO pokemon, float value = 0);
}
