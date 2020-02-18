using System.Collections;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnPeriod;

    protected IEnumerator Start()
    {
        while (true)
        {
            var spawned = pool.Rent(prefab, maxLifetime: 5f);
            spawned.transform.position = transform.position + Random.onUnitSphere;
            
            yield return new WaitForSeconds(spawnPeriod);
        }
    }

}
