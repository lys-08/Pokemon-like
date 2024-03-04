using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Ruby,
    Emerald,
    Sapphire
}

[CreateAssetMenu(fileName = "NewPokemon", menuName = "Pokemon")]
public class PokemonSO : ScriptableObject
{
    public string name;
    public float hp;
    public float damage;
    public float defense;
    public float speed;
    public Type type;
}
