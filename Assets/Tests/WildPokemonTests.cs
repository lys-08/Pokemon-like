using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.AddressableAssets;

public class WildPokemonTests
{
    private GameObject pokemon;
    private WildPokemonSO data;
    
    [UnitySetUp]
    public IEnumerator PokemonInstantiation()
    {
        var loadHandler = Addressables.LoadAssetAsync<GameObject>("Assets/Pokemon/Pokemon1/Pokemon1.prefab");
        yield return loadHandler;
        
        pokemon = GameObject.Instantiate(loadHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
        data = AssetDatabase.LoadAssetAtPath<WildPokemonSO>("Assets/Pokemon/Pokemon2/NewWildPokemon.asset");
        yield return data;
    }

    [UnityTest]
    public IEnumerator WildPokemonMovementsTest()
    {
        // TODO
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator GenerateCoeffsTest()
    {
        data.GenerateCoeffs();
        
        Debug.Log(data.attackCoeff_);
        Debug.Log(data.focusCoeff_);
        Debug.Log(data.runCoeff_);
        Debug.Log(data.distractCoeff_);
        
        Assert.That(data.attackCoeff_, Is.GreaterThanOrEqualTo(50/100f).And.LessThanOrEqualTo(70/100f));
        Assert.That(data.focusCoeff_, Is.GreaterThanOrEqualTo(5/100f).And.LessThanOrEqualTo(1 - data.attackCoeff_));
        Assert.That(data.runCoeff_, Is.GreaterThanOrEqualTo(2/100f).And.LessThanOrEqualTo(4/100f));
        Assert.That(data.distractCoeff_, Is.Not.EqualTo(0f).Within(0.01f));
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator GetObjTest()
    {
        // TODO
        yield return null;
    }
}
