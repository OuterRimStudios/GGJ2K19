using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Car"))
        {
            OutOfControlManager.Instance.RemoveEnemy(other.transform);
            Destroy(other.gameObject);
        }
        else if(other.tag.Equals("Environment"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
