using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "NewWildPokemon", menuName = "NewPokemon/WildPokemon")]
public class WildPokemonSO : PokemonSO
{
    /*
     * Coefficients that determined the percentage of the pokemon to attack, distract and
     * focus during a fight
     */
    public float attackCoeff_;
    public float distractCoeff_;
    public float focusCoeff_;
    public float runCoeff_;
    
    //public Dictionary<int, InventoryItem> objs_; // Possible objects that can be given when the pokemon is KO
    //[field: SerializeField] public List<InventoryItem> objs_;
    public InventoryItem objs_;

    
    
    /**
     * Function returning the item obtained once the pokémon is KO
     * -> Dictionary objects have a probability of optention
     */
    public InventoryItem GetObj()
    {
        /*
         * TODO : revoir le random des objets
         
        int nb = Random.Range(1, 100);

        foreach (var obj in objs_)
        {
            if (obj.Key >= nb) return obj.Value;
        }
        */

        // If no object matches, we return a game object null: the player does not retrieve an item
        return objs_;
    }


    /**
     *  Function that generate the coefficients of the attack the pokemon do in a fight
     * -> this function is called at the creation of the pokemon
     */
    private void GenerateCoeffs()
    {
        attackCoeff_ = Random.Range(50, 70) / 100f;
        focusCoeff_ = Random.Range(5, 1 - attackCoeff_) / 100f;
        runCoeff_ = Random.Range(2, 4) / 100f;
        distractCoeff_ = 1f - attackCoeff_ - focusCoeff_ - runCoeff_;
    }


    #region Unity Event Function

    private void Start()
    {
        GenerateCoeffs();
        Debug.Log("Awake wild pokemon");
    }

    #endregion
}
