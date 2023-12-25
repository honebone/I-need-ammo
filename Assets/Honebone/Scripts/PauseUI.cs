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
    bool onTutorial;
    bool gameover;
    bool f;
    public void TogglePause()
    {
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
        SetTimescale();
    }
    public void SetTutorial(bool f)
    {
        onTutorial = f;
        SetTimescale();
    }
    public void Gameover()
    {
        gameover = true;
        SetTimescale();
    }
    public void SetTimescale()
    {
        if (paused || onTutorial||gameover) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }
    public bool CheckPaused() { return f; }
    public void RestFrag()
    {
        f = false;
    }
}
