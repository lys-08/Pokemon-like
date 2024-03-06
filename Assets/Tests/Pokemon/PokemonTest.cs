using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.AddressableAssets;

public class PokemonTest
{
    private GameObject pokemon;
    
    
    [UnitySetUp]
    public IEnumerator PokemonInstantiation()
    {
        var loadHandler = Addressables.LoadAssetAsync<GameObject>("Assets/Pokemon/Pokemon1/Pokemon1.prefab");
        yield return loadHandler;
      
        pokemon = GameObject.Instantiate(loadHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
    }

    
    [Test]
    public void CoeffTest()
    {
        var pokemonScript = pokemon.GetComponent<Pokemon>();

        /*
         * TakeDistraction
         */
        for (int i = 0; i < 7; i++)
        {
            pokemonScript.TakeDistraction();
        }
        pokemonScript.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(85f).Within(0.01)); // damage = 10 * 1 * 1.5
        
        
        /*
         * ResetCoeff
         */
        pokemonScript.ResetCoeffs();
        pokemonScript.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(75f).Within(0.01));
        
        
        /*
         * TakeDistraction
         */
        for (int i = 0; i < 7; i++)
        {
            pokemonScript.TakeFocus();
        }
        pokemonScript.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(70f).Within(0.01)); // damage = 10 * 1 * 0.5
    }
    

    [Test]
    public void TakeDamageTest()
    {
        var pokemonScript = pokemon.GetComponent<Pokemon>();
        
        pokemonScript.TakeDamage(20f, Type.Sapphire);
        /*
         * hp = 70
         * damage = 20
         * type Sapphire contre type ruby donc damage * 1.5f
         */
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(40f).Within(0.01));
        
        pokemonScript.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(30f).Within(0.01));
        
        pokemonScript.TakeDamage(10f, Type.Emerald);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(25f).Within(0.01));
    }
}
