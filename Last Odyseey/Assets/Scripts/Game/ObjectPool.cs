using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs;

    public GameObject GetObject(string type)
    {
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            if (objectPrefabs[i].name == type)
            {
                // find the reuqired type monster and Instantiate it 
                GameObject newObject = Instantiate(objectPrefabs[i]);
                newObject.name = type;
                // return the Instantiated object to Gamemangager to set the transform position
                return newObject;
            }
        }
        return null; 
    }
}
