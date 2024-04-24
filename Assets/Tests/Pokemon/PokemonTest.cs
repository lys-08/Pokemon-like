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
            //pokemonScript.TakeDistraction();
        }
        //pokemonScript.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(92.5f).Within(0.01)); // 100 - (10 - 5 * 0.5) = 92.5
        
        
        /*
         * ResetCoeff
         */
        //pokemonScript.ResetCoeffs();
        //pokemonScript.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(87.5f).Within(0.01)); // 92.5 - (10 - 5 * 1) = 87.5
        
        
        /*
         * TakeDistraction
         */
        for (int i = 0; i < 7; i++)
        {
            //pokemonScript.TakeFocus();
        }
        //pokemonScript.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(85f).Within(0.01)); // 87.5 - (10 - 5 * 1.5) = 85
        
        //pokemonScript.ResetCoeffs();
    }
    

    [Test]
    public void TakeDamageTest()
    {
        var pokemonScript = pokemon.GetComponent<Pokemon>();
        
        //pokemonScript.TakeDamage(20f, Type.Sapphire);
        /*
         * hp = 85
         * damage = 20
         * defense = 5
         * type Sapphire contre type ruby donc damage * 1.5f
         * total : 85 - (20 * 1.5 - 5) = 
         */
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(60f).Within(0.01));
        
        //pokemonScript.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(55f).Within(0.01)); // 60 - (10 * 1 - 5) = 55
        
        //pokemonScript.TakeDamage(10f, Type.Emerald);
        Assert.That(pokemonScript.GetHp(), Is.EqualTo(55f).Within(0.01)); // 55 - (10 * 0.5 - 5) = 55
    }
}
