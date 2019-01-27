using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeIn : MonoBehaviour
{
    public float fadeSpeed;
    public float fadeDuration;
    public Color fadeToColor;
    Image image;
    float timer;
    bool done;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (timer >= fadeDuration)
        {
            StopAllCoroutines();
            image.color = fadeToColor;
            timer = 0;
        }
    }
    public void StartFade()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitUntil(FadingIn);
    }

    bool FadingIn()
    {
        image.color = Color.Lerp(image.color, fadeToColor, Time.deltaTime * fadeSpeed);
        timer += 1 / 60.0f;
        if (image.color == Color.clear)
            return true;
        else
            return false;
    }
}
