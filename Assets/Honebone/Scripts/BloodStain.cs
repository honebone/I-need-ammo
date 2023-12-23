using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStain : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Color[] colorVariation;
    [SerializeField]
    Sprite[] spriteVariation;
    // Start is called before the first frame update
    void Start()
    {
        sprite.color = colorVariation[Random.Range(0, colorVariation.Length)];
        sprite.sprite = spriteVariation[Random.Range(0, spriteVariation.Length)];
        if (50f.Probability()) { sprite.flipX = true; }
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3f);
        var wait = new WaitForSeconds(0.5f);
        for (int i = 0; i < 15; i++)
        {
            yield return wait;
            Color c = sprite.color;
            c.a -= 0.05f;
            sprite.color = c;
        }

        Destroy(gameObject);
    }
}
