using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private readonly List<GameObject> _pooledObjects = new List<GameObject>();
    [SerializeField] private int amountToPool;
    [SerializeField] private GameObject bulletPrefab;
    private PlayerMovement _playerMovement;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
            
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                _pooledObjects[i].SetActive(true);
                ActivateGameObjectRecursively(_pooledObjects[i]);
                return _pooledObjects[i];
            }
        }
        return null;
    }
    
    // Activating parent object recursively
    void ActivateGameObjectRecursively(GameObject parentObject)
    {
        parentObject.SetActive(true);

        foreach (Transform child in parentObject.transform)
        {
            ActivateGameObjectRecursively(child.gameObject);
        }
    }

    public void ResetObjectPool(GameObject newPrefab, int newAmount)
    {
        //clear the existing pool
        foreach (var obj in _pooledObjects)
        {
            Destroy(obj);
        }
        _pooledObjects.Clear();
        
        // Instantiate new objects on the new prefab and amount
        for (int i = 0; i < newAmount; i++)
        {
            GameObject newObj = Instantiate(newPrefab);
            newObj.SetActive(false);
            _pooledObjects.Add(newObj);
        }
    }
}
