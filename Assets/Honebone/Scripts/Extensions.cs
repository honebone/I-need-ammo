using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static bool CheckRaycastHit(this RaycastHit2D hit, string tagName)
    {
        return hit.collider != null && hit.collider.CompareTag(tagName);
    }
    public static Vector2 UnitCircle(this float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
    public static int ChoiceWithWeight(this List<int> weight)
    {
        float sum = 0;
        foreach (float c in weight)
        {
            sum += c;
        }
        float dice = Random.Range(0, sum);
        //Debug.Log(dice.ToString());
        for (int i = 0; i < weight.Count; i++)
        {
            if (dice < weight[i])
            {
                //Debug.Log(i.ToString());
                return i;
            }
            dice -= weight[i];
        }
        if (dice == sum) { return weight.Count - 1; }
        else
        {
            Debug.Log("error");
            return -1;
        }
    }
    public static bool Probability(this float fPercent)
    {
        bool result;
        float fProbabilityRate = UnityEngine.Random.value * 100.0f;
        // Debug.Log(fProbabilityRate.ToString());
        if (fPercent == 100.0f && fProbabilityRate == fPercent)
        {
            result = true;
        }
        else if (fProbabilityRate < fPercent)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        //if (debug) { Debug.Log("確率：" + fPercent.ToString("N1") + "出目：" + fProbabilityRate.ToString("N1") + "結果：" + result); }
        return result;
    }
}
