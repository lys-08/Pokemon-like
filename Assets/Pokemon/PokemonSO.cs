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
    // features
    public string name;
    public string description;
    public Type type;
    public Sprite image;
    
    // Base stats
    public float hp;
    public float hpMax;
    public float damage;
    public float defense;
    public float speed;
    
    public bool ko;
}
