using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static bool CheckRaycastHit(this RaycastHit2D hit, string tagName)
    {
        return hit.collider != null && hit.collider.CompareTag(tagName);
    }
}
