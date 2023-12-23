using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    GameManager gameManager;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetText(int wave)
    {
        anim.SetTrigger("waveStart");
        text.text = string.Format("ウェーブ{0}", wave);
    }
    public void StartWave()
    {
        gameManager.StartWave();
    }
}
