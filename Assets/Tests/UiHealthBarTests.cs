using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.AddressableAssets;


public class UiHealthBarTests
{
    private GameObject battle;
    private UIHealthBar uiHealthBar;


    [UnitySetUp]
    public IEnumerator SetUpUiBar()
    {
        battle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Battle/[Battle System].prefab");
        uiHealthBar = battle.GetComponentInChildren<UIHealthBar>();

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator SetPokemonTest()
    {
        uiHealthBar.SetPokemon(AssetDatabase.LoadAssetAtPath<WildPokemonSO>("Assets/Pokemon/Zircon/Zircon.asset"));
        
        Assert.True(uiHealthBar.pokemonData.Name == "Zircon");
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator UpdateBarTest()
    {
        PokemonSO poke = AssetDatabase.LoadAssetAtPath<WildPokemonSO>("Assets/Pokemon/Zircon/Zircon.asset");

        yield return uiHealthBar.UpdateBar(50f);
        
        Assert.That(uiHealthBar.pokemonData.hp, Is.EqualTo(50f).Within(0.01));
        yield return null;
    }
}
