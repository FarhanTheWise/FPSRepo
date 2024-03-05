using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    #region Singleton
    public static PoolingManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    #endregion

    #region Pool Class

    [System.Serializable]
    public class Pool
    {
        public string poolTitle;
        public PoolName name;
        public List<GameObject> poolObject;
        public int size;
        public GameObject ObjectGroup;
    }

    #endregion

    #region All Variables

    public bool startPool;
    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    #endregion

    #region Start Method

    public void Start()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pools in Pools)
        {
            GameObject newChild = Instantiate(pools.ObjectGroup);
            newChild.name = pools.name.ToString();
            newChild.transform.parent = transform;
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pools.size; i++)
            {

                GameObject go = Instantiate(pools.poolObject[Random.Range(0, pools.poolObject.Count)]);
                go.transform.parent = newChild.transform;

                go.SetActive(false);
                objectPool.Enqueue(go);

            }

            poolDictionary[pools.name.ToString()] = objectPool;

        }
    }

    #endregion

    #region Spawn pooled object
    public void SpawnObject(Vector3 position, Quaternion rotation, PoolName poolName)
    {

        if (!startPool)
        {
            return;
        }

            GameObject go = poolDictionary[poolName.ToString()].Dequeue();

            go.transform.position = position;
            go.transform.rotation = rotation;

            go.SetActive(true);

            poolDictionary[poolName.ToString()].Enqueue(go);

    }
    public void SpawnObjectBullet(Vector3 position, Quaternion rotation, PoolName poolName, float force, Vector3 direction)
    {

        if (!startPool)
        {
            return;
        }

            GameObject go = poolDictionary[poolName.ToString()].Dequeue();

            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
            go.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(force * direction, ForceMode.Impulse);

            poolDictionary[poolName.ToString()].Enqueue(go);

    }

    #endregion

}

public enum PoolName
{
    Bullet
}
