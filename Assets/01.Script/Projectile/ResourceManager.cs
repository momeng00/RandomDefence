using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{

    private static ResourceManager _instance;
    public static ResourceManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private readonly Dictionary<string, Queue<GameObject>> _objectPool = new();


    public GameObject Spawn(GameObject prefab, Transform parent = null)
    {
        string key = prefab.name;

        if (!_objectPool.ContainsKey(key))
        {
            _objectPool[key] = new Queue<GameObject>();
        }
        
        if (_objectPool[key].Count > 0)
        {
            GameObject pooledObject = _objectPool[key].Dequeue();

            pooledObject.SetActive(true);
            if (parent != null) { pooledObject.transform.SetParent(parent); }
            ResetObject(pooledObject);
            return pooledObject;
        }
        else
        {
            GameObject newObject = Instantiate(prefab, parent);
            newObject.name = key;
            return newObject;
        }
    }




    public void Destroy(GameObject obj)
    {
        if (!obj.activeInHierarchy)
        {
            Debug.LogWarning("이미 풀에 반환된 오브젝트입니다.");
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(transform); 

        if (!_objectPool.ContainsKey(obj.name))
        {
            _objectPool[obj.name] = new Queue<GameObject>();
        }

        _objectPool[obj.name].Enqueue(obj);
    }


    public Queue<GameObject> GetPoolObjects(string key)
    {
        return _objectPool[key];
    }

    
    private void ResetObject(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
}