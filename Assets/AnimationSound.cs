using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    public List<AudioClip> clips;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        source.PlayOneShot(Utilities.GetRandomItem(clips));
    }
}
