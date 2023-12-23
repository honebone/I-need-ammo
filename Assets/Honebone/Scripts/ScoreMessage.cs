using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMessage : MonoBehaviour
{
    [SerializeField]
    Text messageText;

    public void Init(string text)
    {
        messageText.text = text;
    }
    float timer;
    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer > 3)
        {
            Destroy(gameObject);
        }
    }
}
