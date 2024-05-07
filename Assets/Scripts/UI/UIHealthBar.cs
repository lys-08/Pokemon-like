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
    
    public PokemonSO pokemonData; // public for the test


    /**
     * Associate a pokemon with the health bar
     */
    public void SetPokemon(PokemonSO pokemon)
    {
        pokemonData = pokemon;
        
        fillImage.fillAmount = pokemonData.hp / pokemonData.hpMax;
        fillImage.color = Color.Lerp(Color.red, Color.green, fillImage.fillAmount);
        hpText.text = pokemonData.hp + "/" + pokemonData.hpMax;
    }

    /**
     * Coroutine for a smooth animation of the bar
     */
    public IEnumerator UpdateBar(float newHp)
    {
        var duration = fillImage.fillAmount - newHp / pokemonData.hpMax;
        duration = Mathf.Abs(duration);
        var timeLeft = duration;
        
            while (timeLeft > 0)
        {
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, newHp / pokemonData.hpMax, duration - timeLeft);
            fillImage.color = Color.Lerp(Color.red, Color.green, fillImage.fillAmount);
            hpText.text = pokemonData.hp + "/" + pokemonData.hpMax;
            
            yield return null;
            timeLeft -= Time.deltaTime;
        }
    }
}
