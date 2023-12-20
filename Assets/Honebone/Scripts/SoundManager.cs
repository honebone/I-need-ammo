using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    GameObject speaker;
    public void PlaySE(Vector2 pos,AudioClip SE)
    {
        var s = Instantiate(speaker, pos, Quaternion.identity, transform);
        s.GetComponent<Speaker>().Init(SE);
    }
}
