using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PokemonStatModifierSO : ScriptableObject
{
    public abstract bool AffectPokemon(PokemonSO pokemon, float value = 0f, Type type = Type.Simple);
}
