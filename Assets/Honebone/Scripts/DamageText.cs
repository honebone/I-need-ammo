using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float duration;

    float timer;
    public void Init(int DMG)
    {
        text.text = DMG.ToString();
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        transform.Translate(new Vector2(1,1 - timer / duration) * moveSpeed / 50f);
        if (timer >= duration) { Destroy(gameObject); }

        Color c = text.color;
        c.a = (1 - timer / duration);
        text.color = c;
    }
}
