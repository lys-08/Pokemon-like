using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;

public class PokemonTests
{
    private PokemonSO pokemon;
    
    [UnitySetUp]
    public IEnumerator PokemonInstantiation()
    {
        pokemon = AssetDatabase.LoadAssetAtPath<PokemonSO>("Assets/Pokemon/Pokemon1/Pokemon1.asset");
        yield return pokemon;
    }
    
    [UnityTest]
    public IEnumerator PokemonCharacteristicsTest()
    {
        Assert.True(pokemon.Name == "Zircon");
        Assert.True(pokemon.description == "ruby pokemon");
        Assert.True(pokemon.type == Type.Ruby);
        Assert.That(pokemon.hp, Is.EqualTo(200f).Within(0.01f));
        Assert.That(pokemon.hpMax, Is.EqualTo(200f).Within(0.01f));
        Assert.That(pokemon.damage, Is.EqualTo(10f).Within(0.01f));
        Assert.That(pokemon.defense, Is.EqualTo(5f).Within(0.01f));
        Assert.That(pokemon.speed, Is.EqualTo(5f).Within(0.01f));
        pokemon.ResetCoeffs();
        Assert.That(pokemon.damageCoef, Is.EqualTo(1f).Within(0.01f));
        Assert.That(pokemon.defenseCoef, Is.EqualTo(1f).Within(0.01f));
        Assert.False(pokemon.ko);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator TakeDamageTest()
    {
        /*
         * hp -= 10 - 5 * 1
         * => 200 - 5 = 195
         */
        pokemon.TakeDamage(10f, Type.Simple);
        Assert.That(pokemon.hp, Is.EqualTo(195f).Within(0.01f));
        
        /*
         * hp -= 10 - 5 * 1
         * => 195 - 5 = 190
         */
        pokemon.TakeDamage(10f, Type.Ruby);
        Assert.That(pokemon.hp, Is.EqualTo(190f).Within(0.01f));
        
        /*
         * hp -= 50 * 0.5 - 5 * 1
         * => 190 - 20 = 170
         */
        pokemon.TakeDamage(50f, Type.Emerald);
        Assert.That(pokemon.hp, Is.EqualTo(170f).Within(0.01f));
        
        /*
         * hp -= 50 * 1.5 - 5 * 1
         * => 170 - 70 = 100
         */
        pokemon.TakeDamage(50f, Type.Sapphire);
        Assert.That(pokemon.hp, Is.EqualTo(100f).Within(0.01f));
        
        pokemon.TakeDamage(50f, Type.Sapphire);
        pokemon.TakeDamage(50f, Type.Sapphire);
        Assert.True(pokemon.ko);
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator TakeDistractionTest()
    {
        pokemon.TakeDistraction();
        Assert.That(pokemon.damageCoef, Is.EqualTo(0.9f).Within(0.01f));
        Assert.That(pokemon.defenseCoef, Is.EqualTo(0.9f).Within(0.01f));
        
        pokemon.TakeDistraction(); // 0.8
        pokemon.TakeDistraction(); // 0.7
        pokemon.TakeDistraction(); // 0.6
        pokemon.TakeDistraction(); // 0.5
        pokemon.TakeDistraction(); // 0.4 => 0.5
        Assert.That(pokemon.damageCoef, Is.EqualTo(0.5f).Within(0.01f));
        Assert.That(pokemon.defenseCoef, Is.EqualTo(0.5f).Within(0.01f));
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator ResetCoeffsTest()
    { 
        pokemon.ResetCoeffs();
        
        Assert.That(pokemon.damageCoef, Is.EqualTo(1f).Within(0.01f));
        Assert.That(pokemon.defenseCoef, Is.EqualTo(1f).Within(0.01f));
        
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator FocusTest()
    {
        pokemon.Focus();
        Assert.That(pokemon.damageCoef, Is.EqualTo(1.1f).Within(0.01f));
        Assert.That(pokemon.defenseCoef, Is.EqualTo(1.1f).Within(0.01f));
        
        pokemon.Focus(); // 1.2
        pokemon.Focus(); // 1.3
        pokemon.Focus(); // 1.4
        pokemon.Focus(); // 1.5
        pokemon.Focus(); // 1.6 => 1.5
        Assert.That(pokemon.damageCoef, Is.EqualTo(1.5f).Within(0.01f));
        Assert.That(pokemon.defenseCoef, Is.EqualTo(1.5f).Within(0.01f));
        
        yield return null;
    }
}
