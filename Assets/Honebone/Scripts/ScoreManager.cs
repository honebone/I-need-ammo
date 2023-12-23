using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    GameObject message;
    [SerializeField]
    Transform messageP;

    public void AddScore(float s, string m,bool noteScore)
    {
        score += Mathf.RoundToInt(s);
        string me = m;
        if (noteScore) { me += " +" + Mathf.RoundToInt(s).ToString(); }
        scoreText.text = string.Format("SCORE:{0}", score);
        if (me != "")
        {
            var t = Instantiate(message, messageP);
            t.GetComponent<ScoreMessage>().Init(me);
        }
    }
}
