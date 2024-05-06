using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;

public class Spawner : MonoBehaviour
{
    [SerializeField, Range(1, 3)] private int Zone = 1;

    private float timeToSpawn = 30.0f;


    private AsyncOperationHandle<IList<GameObject>> loadHandle;
    private UnityEvent<GameObject> onSpawn = new UnityEvent<GameObject>();

    void Start()
    {
        timeToSpawn = Random.Range(25.0f, 30.0f);
        
        if (Zone == 1)
        {
            loadHandle = Addressables.LoadAssetsAsync<GameObject>("PokemonRuby", null);
        }
        else if (Zone == 2)
        {
            loadHandle = Addressables.LoadAssetsAsync<GameObject>("PokemonEmerald", null);
        }
        else if (Zone == 3)
        {
            loadHandle = Addressables.LoadAssetsAsync<GameObject>("PokemonSapphire", null);
        }
        
        loadHandle.Completed += (operation) => { StartCoroutine(SpawnEnemy()); };
    }

    public void AddOnSpawnListener(UnityAction<GameObject> listener)
    {
        onSpawn.AddListener(listener);
    }


    private void OnDestroy()
    {
        Addressables.Release(loadHandle);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);

            timeToSpawn = Random.Range(30.0f, 35.0f);

            var increment = Random.insideUnitCircle * 5;
            var spawnPosition = new Vector3(transform.position.x + increment.x, transform.position.y, transform.position.z + increment.y);

            var enemyPrefab = loadHandle.Result[Random.Range(0, loadHandle.Result.Count)];
            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
            onSpawn.Invoke(enemy);
        }
    }
}
