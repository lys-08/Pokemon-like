using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    /*
     * Player information
     */
    [SerializeField] private TextMeshProUGUI playerPokemonName;
    [SerializeField] private UIHealthBar playerPokemonBar;
    [SerializeField] private Image playerPokemonImage;
    
    /*
     * Wild Pokemon information
     */
    [SerializeField] private TextMeshProUGUI pokemonName;
    [SerializeField] private UIHealthBar pokemonBar;
    [SerializeField] private Image pokemonImage;


    /**
     * Set data of the opposed pokemon in the fight
     */
    public void SetData(PokemonSO playerPokemon, PokemonSO wildPokemon)
    {
        // Player pokemon
        playerPokemonName.text = playerPokemon.name;
        playerPokemonBar.SetPokemon(playerPokemon);
        playerPokemonImage.sprite = playerPokemon.image;
        
        // Wild Pokemon
        pokemonName.text = wildPokemon.name;
        pokemonBar.SetPokemon(wildPokemon);
        pokemonImage.sprite = wildPokemon.image;
    }
}
