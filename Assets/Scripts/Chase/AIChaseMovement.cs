using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseMovement : MonoBehaviour
{
    public float speed;
    public float startDelay;

    bool start;
    bool waitForDelay;

    private void Start()
    {
        waitForDelay = true;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(startDelay);
        start = true;
    }

    private void Update()
    {
        if(waitForDelay)
        {
            if(Input.GetAxis("Horizontal") != 0)
            {
                waitForDelay = false;
                StartCoroutine(Delay());
            }
        }

        if (!start) return;
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            GameManager.Instance.GameOver();
    }
}
