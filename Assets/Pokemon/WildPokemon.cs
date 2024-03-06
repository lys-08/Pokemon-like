using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    
    public GameObject GetObj()
    {
        // TODO
        return objs_[0];
    }


    private void LaunchFight()
    {
        // TODO
    }
}
