using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPokemon : MonoBehaviour
{
    [SerializeField] private WildPokemonSO data;

    private float timeToDeSpawn = 30.0f;


    private void Awake()
    {
        timeToDeSpawn = Random.Range(25.0f, 30.0f);
        Destroy(gameObject, timeToDeSpawn);
    }

    public WildPokemonSO getData()
    {
        return data;
    }

}
