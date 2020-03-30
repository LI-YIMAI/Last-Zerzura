using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs;
    private List<GameObject> pooledObjects = new List<GameObject>();
    public GameObject GetObject(string type)
    {
        foreach (GameObject go in pooledObjects)
        {
            if(go.name == type && !go.activeInHierarchy)
            {
                go.SetActive(true);
                return go;
            }
        }
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            if (objectPrefabs[i].name == type)
            {
                // find the reuqired type monster and Instantiate it 
                GameObject newObject = Instantiate(objectPrefabs[i]);
                pooledObjects.Add(newObject);
                newObject.name = type;
                // return the Instantiated object to Gamemangager to set the transform position
                return newObject;
            }
        }
        return null; 
    }

    public void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
