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

    public List<InventoryItem> objs;

    public List<int> coefs;

    
    
    /**
     * Function returning the item obtained once the pokémon is KO
     * -> Dictionary objects have a probability of optention
     */
    public InventoryItem GetObj()
    {
        // DONE : revoir le random des objets

        if (objs.Count != coefs.Count) throw new Exception("The number of objects and the number of coefficients must be the same");

        var ListObjs = new List<InventoryItem>();

        foreach (var obj in objs)
        {
            for (int i = 0; i < coefs[objs.IndexOf(obj)]; i++)
            {
                ListObjs.Add(obj);
            }
        }

        return ListObjs[Random.Range(0, ListObjs.Count)];
    }


    /**
     *  Function that generate the coefficients of the attack the pokemon do in a fight
     * -> this function is called at the creation of the pokemon
     */
    public void GenerateCoeffs()
    {
        attackCoeff_ = Random.Range(50, 70) / 100f;
        focusCoeff_ = Random.Range(5/ 100f, 1 - attackCoeff_) ;
        runCoeff_ = Random.Range(2, 4) / 100f;
        distractCoeff_ = 1f - attackCoeff_ - focusCoeff_ - runCoeff_;
    }


    #region Unity Event Function

    public void Initialization()
    {
        GenerateCoeffs();
        Debug.Log("Awake wild pokemon");
    }

    #endregion
}
