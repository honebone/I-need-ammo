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

    public void TogglePause()
    {
        gameManager.TogglePause();
        if (image.sprite == pause) { image.sprite = resume; }
        else { image.sprite = pause; }
    }
}
