using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OutOfControlManager.Instance.RemoveEnemy(other.transform);
        Destroy(other.gameObject);
    }
}
