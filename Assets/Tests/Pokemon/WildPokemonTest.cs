using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.AddressableAssets;

public class WildPokemonTest
{
    private GameObject pokemon;
    
    
    [UnitySetUp]
    public IEnumerator PokemonInstantiation()
    {
        var loadHandler = Addressables.LoadAssetAsync<GameObject>("Assets/Pokemon/Pokemon2/Pokemon2.prefab");
        yield return loadHandler;
      
        pokemon = GameObject.Instantiate(loadHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    
    [Test]
    public void GetObjTest()
    {
        // TODO
    }
    
    [Test]
    public void LaunchFightTest()
    {
        // TODO
    }
    
    [Test]
    public void GenerateCoeffsTest()
    {
        /*var wildPokemonScript = pokemon.GetComponent<WildPokemon>();
        
        Debug.Log(wildPokemonScript.GetAttackCoeff());
        Debug.Log(wildPokemonScript.GetDistractCoeff());
        Debug.Log(wildPokemonScript.GetFocusCoeff());
        
        Assert.That(wildPokemonScript.GetAttackCoeff(), Is.InRange(0.5, 0.7));
        Assert.That(wildPokemonScript.GetDistractCoeff(), Is.InRange(0.05, 1 - wildPokemonScript.GetAttackCoeff()));

        var sum = wildPokemonScript.GetAttackCoeff() + wildPokemonScript.GetDistractCoeff() +
                  wildPokemonScript.GetFocusCoeff();
        Assert.That(sum, Is.EqualTo(1f).Within(0.01))*/    }
}
