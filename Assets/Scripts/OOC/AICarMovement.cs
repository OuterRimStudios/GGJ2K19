using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarMovement : MonoBehaviour
{
    public float speed;
    public AudioSource hornSource;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            //hornSource.PlayOneShot(hornSource.clip);
            GameManager.Instance.GameOver();
            OutOfControlManager.Instance.GameOver();
        }
    }
}
