using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;

public class PlayerTests
{
    private GameObject player;
    
    [UnitySetUp]
    public IEnumerator PlayerInstantiation()
    {
        var loadHandler = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Player/PLAYER.prefab");
        yield return loadHandler;
      
        player = GameObject.Instantiate(loadHandler, new Vector3(0, 0, 0), Quaternion.identity);
        yield return null;
    }


    [UnityTest]
    public IEnumerator PlayerControlsTest()
    {
        Mover playerMover = player.GetComponent<Mover>();
        
        var start = Time.time;
        while (Time.time - start < 2f)
        {
            playerMover.Move(player.transform.forward * 5f * Time.deltaTime);
            yield return null;
        }
  
        Assert.That(player.transform.position.z, Is.GreaterThan(10f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerPokemonEncounterTest()
    {
        // TODO
        yield return null;
    }
}
