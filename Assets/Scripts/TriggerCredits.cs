using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCredits : MonoBehaviour
{
    public GameObject winScenario;
    public GameObject credits;

    Animator animator;

    private IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        yield return new WaitForSeconds(6);
        animator.SetTrigger("Alarm");
    }

    public void RollTheCredits()
    {
        winScenario.SetActive(false);
        credits.SetActive(true);
    }
}
