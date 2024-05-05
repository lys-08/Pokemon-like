using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public event Action<WildPokemonSO> OnEncountered; // Action apply when the player encounter a pokemon


    public void HandleUpdate()
    {
        /* TODO
        var colliders = Physics.OverlapSphere(transform.position, 5f, 6);
        if (colliders != null)
        {
            OnEncountered?.Invoke(colliders[0].gameObject.GetComponent<WildPokemonSO>());
        }*/
    }
}
