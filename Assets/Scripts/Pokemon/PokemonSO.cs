using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum Type
{
    Simple,
    Ruby,
    Emerald,
    Sapphire
}

[CreateAssetMenu(fileName = "NewPokemon", menuName = "NewPokemon/Pokemon")]
public class PokemonSO : ScriptableObject
{
    // features
    [FormerlySerializedAs("name")] public string Name;
    public string description;
    public Type type;
    public Sprite image;
    
    // Base stats
    public float hp;
    public float hpMax;
    public float damage;
    public float defense;
    public float speed;
    // Coefficients apply in a fight
    public float damageCoef = 1f; 
    public float defenseCoef = 1f;
    
    public bool ko;
    public bool evol; // if the true, the pokemon is an evolution of a less powerfull pokemon


    private void Start()
    {
        // SetCoefs();
    }

    /**
     * Get the damage done according to the current coefs
     */
    public float GetDamage()
    {
        return damage * damageCoef;
    }
    
    /**
     * Pokemon damage function. The damage dealt depends on the type of the attacking pokémon relative
     * to the type of the current pokemon, as well as the original power of the attack
     *
     * Return the new hp of the pokemon
     */
    public float TakeDamage(float damage, Type type)
    {
        float damageInflicted;
        switch (type)
        {
            case (Type.Ruby):
                if (this.type == Type.Sapphire) damageInflicted = 0.5f * damage;
                else if (this.type == Type.Emerald) damageInflicted = 1.5f * damage;
                else damageInflicted = damage; // (type == Type.Ruby)
                break;
            case (Type.Sapphire):
                if (this.type == Type.Ruby) damageInflicted = 1.5f * damage;
                else if (this.type == Type.Emerald) damageInflicted = 0.5f * damage;
                else damageInflicted = damage; // (type == Type.Sapphire)
                break;
            case (Type.Emerald):
                if (this.type == Type.Sapphire) damageInflicted = 1.5f * damage;
                else if (this.type == Type.Ruby) damageInflicted = 0.5f * damage;
                else damageInflicted = damage; // (type == Type.Emerald)
                break;
            default:
                damageInflicted = damage;
                break;
        }

        /*
         * We apply the damage according of the defense coefficient of the pokemon
         */
        hp -= (int)(damageInflicted - defense * defenseCoef);
        
        // KO
        if (hp <= 0)
        {
            ko = true;
            hp = 0;
        }

        return hp;
    }
    
    /**
     * Reduces the pokemon attack and defense by a percentage for battle duration
     * -> used when the opposed pokemon uses a distraction attack
     */
    public void TakeDistraction()
    {
        /*
         * If the damage coefficient is lesser or equal to 0.5f then we do nothing
         * -> For the condition, we can test only one out of the two value because they
         *    are modified at the same time, by the same percentage
         */
        if (damageCoef <= 0.5f) return;
        
        damageCoef -= 0.1f;
        defenseCoef -= 0.1f;
    }

    /**
     * Increased the pokemon attack and defense by a percentage for battle duration
     * -> used when the pokemon uses a focusing attack
     */
    public void Focus()
    {
        /*
         * If the damage coefficient is greater or equal to 1.5f then we do nothing
         * -> For the condition, we can test only one out of the two value because they
         *    are modified at the same time, by the same percentage
         */
        if (damageCoef >= 1.5f) return;
        
        damageCoef += 0.1f;
        defenseCoef += 0.1f;
    }


    /**
     * Reset all values modified during a fight which are specific to a fight
     * This function is called at the end of a fight
     */
    public void ResetCoeffs()
    {
        damageCoef = 1f;
        defenseCoef = 1f;
    }


    /**
     * Set the coefficients of the pokemon
     * -> called when the pokemon is created
     */
    // public void SetCoefs()
    // {
    //     if (evol)
    //     {  
    //         hp = Random.Range(250, 300);
    //         hpMax = hp;
    //         damage = Random.Range(40, 60);
    //         defense = Random.Range(25, 35);
    //         speed = Random.Range(2, 5);
    //     }
    //     else
    //     {
    //         hp = Random.Range(100, 200);
    //         hpMax = hp;
    //         damage = Random.Range(20, 40);
    //         defense = Random.Range(10, 15);
    //         speed = Random.Range(7, 15); // smaller pokemon are speedier
    //     }
    // }
}
