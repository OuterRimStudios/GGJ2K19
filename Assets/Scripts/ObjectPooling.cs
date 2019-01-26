using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    [Tooltip("The object you want to pool.")]
    public GameObject[] pooledObject;
    [Tooltip("How many of this object you want to pool.")]
    public int pooledAmount;
    [Tooltip("In the case that you don't have enough objects do you want the system to create more?")]
    public bool willGrow = true;

    float waitTime = 0.001f; //The time between generating each object.
    List<GameObject> availableObjects = new List<GameObject>(); //All of the pooled objects.
    List<GameObject> objectsInUse = new List<GameObject>();

    void Start()
    {
        //Creates the amounts of objects you wanted, childs them, disables them, and adds them to a list.
        for (int i = 0; i < pooledAmount; i++)
            StartCoroutine(InstantiatePool(waitTime));
    }

    public GameObject GetPooledObject()
    {
        //If there are any objects that are currently being used
        if (objectsInUse.Count > 0)
        {
            for (int i = 0; i < objectsInUse.Count; i++)
            {
                //Check to see if the objects that were being used are still being used.
                if (!objectsInUse[i].activeInHierarchy)
                {
                    //If the object is no longer being used then add it back to the availableObjects list so we can reuse it later.
                    availableObjects.Add(objectsInUse[i]);
                    objectsInUse.Remove(objectsInUse[i]);
                }
            }
        }

        if (availableObjects.Count > 0) //If there are objects available then 
        {
            //Pick a random object from the availableObjects list
            GameObject obj = availableObjects[Random.Range(0, availableObjects.Count)];
            if (obj)
            {
                //Remove it from the available objects list and put it into the objects in use list
                availableObjects.Remove(obj);
                objectsInUse.Add(obj);
            }
            return obj; //Return the object
        }
        else //If all objects in the list are being used and you are allowing the system to make more then make a new object and return it.
        {
            if (willGrow)
            {
                GameObject obj = Instantiate(pooledObject[Random.Range(0, availableObjects.Count)]);   //Pick a random object (enemy) to spawn
                obj.transform.parent = transform;
                availableObjects.Add(obj);
                return obj;
            }
        }

        Debug.LogError("All of the objects are being used. Either increase the pooledAmount or make sure you have willGrow checked.");
        return null;
    }

    IEnumerator InstantiatePool(float waitTime)
    {
        for (int i = 0; i < pooledObject.Length; i++)   //For each different type of object (enemy)
        {
            //Spawn the object.
            GameObject obj = Instantiate(pooledObject[i]);
            //Child the new object to this gameObject.
            obj.transform.parent = transform;

            //Disable the new object.
            obj.SetActive(false);

            //Add this new object to your list of pooled objects.
            availableObjects.Add(obj);
        }

        //Wait this long before spawning the next object.
        yield return new WaitForSeconds(waitTime);
    }
}