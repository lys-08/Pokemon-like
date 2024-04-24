using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action/*<WildPokemonSO>*/ OnEncountered; // Action apply when the player encounter a pokemon
    
    
    /**
     * Check if the player is on range with a wild pokemon. If he is, then a fight is launched
     */
    private void CheckForEncounters()
    {
        var colliders = Physics.OverlapSphere(transform.position, 5f, 6);
        if (colliders != null)
        {
            OnEncountered?.Invoke();
        }
    }


    private void Update()
    {
        CheckForEncounters();
    }
}
