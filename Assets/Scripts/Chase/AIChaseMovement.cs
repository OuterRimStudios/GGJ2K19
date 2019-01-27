using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseMovement : MonoBehaviour
{
    public float speed;
    public float startDelay;
    public Transform player;
    public Animator animator;

    bool start;
    bool waitForDelay;

    private void Start()
    {
        float distance = Utilities.CheckDistance(transform.position, player.position);
        animator.SetFloat("Distance", 100);

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

        float distance = Utilities.CheckDistance(transform.position, player.position);
        animator.SetFloat("Distance", distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            GameManager.Instance.GameOver();
    }
}
