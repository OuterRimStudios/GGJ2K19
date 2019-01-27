using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject outOfControlGame;
    public GameObject chaseGame;

    public GameObject gameOverRoom;
    public Animator gameOverAnimator;
    public Animator cameraAnimator;
    public PostProcess.BlinkEffect blinkEffect;

    private void Awake()
    {
        Instance = this;
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public void Transition()
    {
        blinkEffect.Blink();
        outOfControlGame.SetActive(false);
        chaseGame.SetActive(true);
    }

    public void GameOver()
    {
        outOfControlGame.SetActive(false);
        chaseGame.SetActive(false);
        gameOverRoom.SetActive(true);
        gameOverAnimator.SetBool("GameOver", true);
        cameraAnimator.SetBool("GameOver", true);
    }
}