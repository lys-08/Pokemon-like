using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WildPokemon : Pokemon
{
    /*
     * Coefficients that determined the percentage of the pokemon to attack, distract and
     * focus during a fight
     */
    private float attackingCoeff_;
    private float distractCoeff_;
    private float focusCoeff_;
    
    private Dictionary<int, GameObject> objs_; // Possible objects that can be given when the pokemon is KO

    private GameObject player_;


    #region Getters

    public float GetAttackingCoeff()
    {
        return attackingCoeff_;
    }

    public float GetDistractCoeff()
    {
        return distractCoeff_;
    }

    public float GetFocusCoeff()
    {
        return focusCoeff_;
    }

    #endregion
    
    
    
    /**
     * Function returning the item obtained once the pokémon is KO
     * -> Dictionary objects have a probability of optention
     */
    public GameObject GetObj()
    {
        int nb = Random.Range(1, 100);

        foreach (var obj in objs_)
        {
            if (obj.Key >= nb) return obj.Value;
        }

        // If no object matches, we return a game object null: the player does not retrieve an item
        return null;
    }


    /**
     * Function that detects if the player is in the pokemon range. If he is, than a fight is launch
     */
    private void LaunchFight()
    {
        // TODO
    }


    /**
     *  Function that generate the coefficients of the attack the pokemon do in a fight
     * -> this function is called at the creation of the pokemon
     */
    private void GenerateCoeffs()
    {
        attackingCoeff_ = Random.Range(50, 70) / 100f;
        focusCoeff_ = Random.Range(attackingCoeff_, 85) / 100f;
        distractCoeff_ = 1f - attackingCoeff_ - focusCoeff_;
    }


    #region Unity Event Function

    private void Awake()
    {
        player_ = GameObject.FindWithTag("Player");
        GenerateCoeffs();
    }

    private void Update()
    {
        /*
         * If the player is in the range of the pokemon, than a fight start
         */
        if (Vector3.Distance(transform.position, player_.transform.position) > 5f) return;
        LaunchFight();
    }

    #endregion
}
