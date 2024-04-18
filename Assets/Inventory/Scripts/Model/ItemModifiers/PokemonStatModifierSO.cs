using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PokemonStatModifierSO : ScriptableObject
{
    public abstract void AffectPokemon(GameObject pokemon, float value);
}
