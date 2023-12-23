using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour
{
    [SerializeField]
    Text logText;

    public void Init(string text,Color color)
    {
        logText.text=text;
        logText.color=color;
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
