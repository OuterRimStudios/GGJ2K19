using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitUntil(FadingOut);
    }

    bool FadingOut()
    {
        image.color = Color.Lerp(image.color, Color.clear, Time.deltaTime * 2f);
        if (image.color == Color.clear)
            return true;
        else
            return false;
    }
}