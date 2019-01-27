using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private void Start()
    {
        float rotation = Random.Range(0, 361);
        transform.Rotate(new Vector3(0, rotation, 0));
    }
}
