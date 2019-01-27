using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutOfControlManager : MonoBehaviour
{
    public static OutOfControlManager Instance;
    public Slider distanceSlider;
    public TextMeshProUGUI distanceText;
    public Animator speedometerAnimator;
    public float animationSmoothTime;

    public float timeScaleMax = 1.5f;

    public float timeToHome = 30f;
    float startTime;
    
    public Animator house;

    [HideInInspector]
    public List<Transform> activeEnemies;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
        speedometerAnimator.SetFloat("Speed", Time.timeScale);
    }

    private void Start()
    {
        StartCoroutine(GPSTimer());
        StartCoroutine(ChangeTimeScale());
    }

    IEnumerator GPSTimer()
    {
        startTime = Time.time;
        yield return new WaitUntil(CheckTime);
        CarMovement.Instance.Arrived();
        CarSpawner.Instance.CanSpawn = false;
        Time.timeScale = 1;     //reset timescale
        StopAllCoroutines();

        house.SetTrigger("Land");
    }

    bool CheckTime()
    {
        float timeElapsed = Time.time - startTime;

        //Not home yet
        if (timeElapsed < timeToHome)
        {
            distanceText.text = (1000 - (int)((timeElapsed / timeToHome) * 1000)).ToString() + " m";
            distanceSlider.value = (timeElapsed / timeToHome);
            return false;
        }
        else
            return true;    //home
    }

    IEnumerator ChangeTimeScale()
    {
        for(; ; )
        {
            yield return new WaitForSecondsRealtime(15f);
            Time.timeScale = Random.Range(1, timeScaleMax);
            speedometerAnimator.SetFloat("Speed", Time.timeScale, animationSmoothTime, Time.unscaledDeltaTime);
        }
    }

    public void AddEnemy(Transform enemy)
    {
        if (!activeEnemies.Contains(enemy))
            activeEnemies.Add(enemy);
    }

    public void RemoveEnemy(Transform enemy)
    {
        if (activeEnemies.Contains(enemy))
            activeEnemies.Remove(enemy);
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }
}