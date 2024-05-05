using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;

public class Spawner : MonoBehaviour
{
    [SerializeField, Range(1, 3)]
    private int Zone = 1;


    private AsyncOperationHandle<IList<GameObject>> loadHandle;
    private UnityEvent<GameObject> onSpawn = new UnityEvent<GameObject>();

    void Start()
    {
        loadHandle = Addressables.LoadAssetsAsync<GameObject>("enemy", null);
        loadHandle.Completed += (operation) => { StartCoroutine(SpawnEnemy()); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDestroy()
    {
        Addressables.Release(loadHandle);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);

            var enemyPrefab = loadHandle.Result[Random.Range(0, loadHandle.Result.Count)];
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
            onSpawn.Invoke(enemy);
        }
    }
}
