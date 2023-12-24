using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    GameObject speaker;

    List<AudioClip> playedSE;
     public void PlaySE(Vector2 pos,AudioClip SE)
    {
        if (!playedSE.Contains(SE))
        {
            playedSE.Add(SE);
            var s = Instantiate(speaker, pos, Quaternion.identity, transform);
            s.GetComponent<Speaker>().Init(SE);
        }
        
    }
    private void FixedUpdate()
    {
        playedSE = new List<AudioClip>();
    }
}
