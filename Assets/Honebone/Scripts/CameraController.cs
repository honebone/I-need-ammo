using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    int horizontalKey;
    int verticalKey;
    float speedMod;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D)) { horizontalKey = 1; }//‰¡ˆÚ“®‚ÌŒŸo 
        else if (Input.GetKey(KeyCode.A)) { horizontalKey = -1; }
        else { horizontalKey = 0; }

        if (Input.GetKey(KeyCode.W)) { verticalKey = 1; }//cˆÚ“®‚ÌŒŸo
        else if (Input.GetKey(KeyCode.S)) { verticalKey = -1; }
        else { verticalKey = 0; }

        if (horizontalKey != 0 && verticalKey != 0) { speedMod = 1 / Mathf.Sqrt(2); }
        else { speedMod = 1f; }
        if (Input.GetKey(KeyCode.LeftShift)) { speedMod *= 3; }

        transform.Translate(moveSpeed * speedMod * new Vector2(horizontalKey, verticalKey));//ˆÚ“®
    }
}
