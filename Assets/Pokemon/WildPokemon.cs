using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPokemon : Pokemon
{
    private float attackingCoeff_;
    private float defenseCoeff_;
    private float focusCoeff_;
    private Dictionary<int, GameObject> objs_;

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
