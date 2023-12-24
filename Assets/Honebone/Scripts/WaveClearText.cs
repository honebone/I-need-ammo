using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveClearText : MonoBehaviour
{
    [SerializeField]
    Text clear;
    [SerializeField]
    Text reward;
    [SerializeField]
    GameManager gameManager;

    public void Clear(string r)
    {
        if (r == "") { reward.text = "<•ñV>\n–³‚µ"; }
        else { reward.text = " < •ñV >\n" +r; }
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        clear.color = Color.yellow;
        yield return new WaitForSeconds(1.5f);
        reward.color = Color.yellow;
        yield return new WaitForSeconds(2f);
        //Color c = Color.yellow;
        //for (int i = 0; i < 20; i++)
        //{
        //    yield return new WaitForSeconds(0.05f);
        //    c.a -= 0.05f;
        //    clear.color = c;
        //    reward.color = c;
        //}
        clear.color = Color.clear;
        reward.color = Color.clear;

        gameManager.NextWave();
    }
}
