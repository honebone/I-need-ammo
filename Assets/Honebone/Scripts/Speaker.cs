using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    bool f;
    public void Init(AudioClip SE)
    {
        audioSource.PlayOneShot(SE);
        f = true;
    }

    void Update()
    {
        if (f && !audioSource.isPlaying) { Destroy(gameObject); }
    }
}
