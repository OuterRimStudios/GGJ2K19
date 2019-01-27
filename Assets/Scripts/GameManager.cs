using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject mainMenu;
    public GameObject outOfControlGame;
    public GameObject chaseGame;
    public GameObject winRoom;
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
        outOfControlGame.SetActive(false);
        chaseGame.SetActive(true);
    }

    public void Win()
    {
        StartCoroutine(WinTransition());
    }

    IEnumerator WinTransition()
    {
        chaseGame.SetActive(false);
        winRoom.SetActive(true);
        yield return new WaitForSeconds(4);
        //Alarm stuff
    }

    public void GameOver()
    {
        outOfControlGame.SetActive(false);
        chaseGame.SetActive(false);
        gameOverRoom.SetActive(true);
        gameOverAnimator.SetBool("GameOver", true);
        cameraAnimator.SetBool("GameOver", true);
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        outOfControlGame.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}