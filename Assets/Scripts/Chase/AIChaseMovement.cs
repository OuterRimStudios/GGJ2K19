using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseMovement : MonoBehaviour
{
    public float speed;
    public float startDelay;

    bool start;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelay);
        start = true;
    }

    private void Update()
    {
        if (!start) return;
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            GameManager.Instance.GameOver();
    }
}
