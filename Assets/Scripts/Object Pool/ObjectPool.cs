﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    private readonly Dictionary<int, Queue<PooledObject>> _poolsByPrefabId = new Dictionary<int, Queue<PooledObject>>();
    [SerializeField] private int poolChunkSize = 5;

    public GameObject Rent(GameObject prefab, float maxLifetime) 
        => Rent(prefab.transform, maxLifetime).gameObject;
    
    public T Rent<T>(T prefab, float maxLifetime) where T : Component
    {
        var id = prefab.GetInstanceID();
        EnsurePoolContains(prefab, id);

        var ret = _poolsByPrefabId[id].Dequeue();
        ret.OnRent(maxLifetime);
        return ret.GetComponent<T>();
    }
    
    public void Return(GameObject obj) 
        => Return(obj.GetComponent<PooledObject>());
    
    public void Return(Component obj) 
        => Return(obj.GetComponent<PooledObject>());

    private void Return(PooledObject obj)
    {
        _poolsByPrefabId[obj.PrefabId].Enqueue(obj);
        obj.OnReturn();
    }

    private void EnsurePoolContains<T>(T prefab, int id) where T : Component
    {
        if (!_poolsByPrefabId.ContainsKey(id))
            _poolsByPrefabId.Add(id, new Queue<PooledObject>());
        
        if (_poolsByPrefabId[id].Count == 0)
            PopulatePool(prefab, id);
    }
    
    private void PopulatePool<T>(T prefab, int id) where T : Component
    {
        for (var i = 0; i < poolChunkSize; ++i)
        {
            var instance = Instantiate(prefab, transform, true);
            instance.name = prefab.name + "(Pooled)";

            var pooledObj = instance.gameObject.AddComponent<PooledObject>();
            pooledObj.Init(id, this);

            _poolsByPrefabId[id].Enqueue(pooledObj);
        }
    }
}
