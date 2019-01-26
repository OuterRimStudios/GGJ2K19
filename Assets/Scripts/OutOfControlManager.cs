using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutOfControlManager : MonoBehaviour {

    public Slider distanceSlider;
    public TextMeshProUGUI distanceText;

    public float timeScaleMax = 1.5f;

    public float timeToHome = 30f;
    float startTime;

    private void Awake()
    {
        Time.timeScale = 1;
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
        print("at home");
        Time.timeScale = 1;     //reset timescale
        StopAllCoroutines();
    }

    bool CheckTime()
    {
        float timeElapsed = Time.time - startTime;

        //Not home yet
        if (timeElapsed < timeToHome)
        {
            distanceText.text = ((int)((timeElapsed / timeToHome) * 1000)).ToString() + " m";
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
            print(Time.timeScale);
        }
    }

    public static void GameOver()
    {

    }
}