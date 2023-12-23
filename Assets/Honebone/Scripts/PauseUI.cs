using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    Sprite pause;

    [SerializeField]
    Sprite resume;
    [SerializeField]
    GameManager gameManager;

    bool paused;
    bool f;
    public void TogglePause()
    {
        gameManager.TogglePause();
        if (!paused)
        {
            f = true;
            paused = true;
            image.sprite = resume;
        }
        else
        {
            paused = false;
            image.sprite = pause;
        }
    }
    public bool CheckPaused() { return f; }
    public void RestFrag()
    {
        f = false;
    }
}
