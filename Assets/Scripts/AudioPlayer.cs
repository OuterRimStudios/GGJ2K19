using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> clips;
    public float minFreq;
    public float maxFreq;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    IEnumerator WaitToPlay()
    {
        for (; ;)
        {
            float waitTime = Random.Range(minFreq, maxFreq);
            yield return new WaitForSecondsRealtime(waitTime);
            source.PlayOneShot(Utilities.GetRandomItem(clips));
            yield return new WaitUntil(() => source.isPlaying == false);
        }
    }
}
