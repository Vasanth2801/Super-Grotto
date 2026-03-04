using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject poolPrefab;
        public int poolSize;
        public string poolName;
    }

    public ObjectPoolItem[] pools;
    public Dictionary<string, Queue<GameObject>> poolsOfDictionary;

    void Start()
    {
        poolsOfDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(ObjectPoolItem pool  in pools)
        {
            Queue<GameObject> obj = new Queue<GameObject>();

            for(int i=0; i< pool.poolSize; i++)
            {
                GameObject objPool = Instantiate(pool.poolPrefab);
                objPool.SetActive(false);
                obj.Enqueue(objPool);
            }

            poolsOfDictionary.Add(pool.poolName, obj);
        }
    }

    public GameObject SpawnFromPools(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject objToSpawn = poolsOfDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolsOfDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
