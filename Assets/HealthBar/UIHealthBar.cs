using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI hpText;
    
    private PokemonSO pokemonData;


    public void SetPokemon(PokemonSO pokemon)
    {
        pokemonData = pokemon;
    }
    
    
    #region Unity Events Methods

    private void Update()
    {
        fillImage.fillAmount = pokemonData.hp / pokemonData.hpMax;
        fillImage.color = Color.Lerp(Color.red, Color.green, fillImage.fillAmount);
        hpText.text = pokemonData.hp + "/" + pokemonData.hpMax;
    }

    #endregion
}
