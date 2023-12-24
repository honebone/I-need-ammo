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
    float wheel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //private void FixedUpdate()
    //{
    //    if (Input.GetKey(KeyCode.D)) { horizontalKey = 1; }//横移動の検出 
    //    else if (Input.GetKey(KeyCode.A)) { horizontalKey = -1; }
    //    else { horizontalKey = 0; }

    //    if (Input.GetKey(KeyCode.W)) { verticalKey = 1; }//縦移動の検出
    //    else if (Input.GetKey(KeyCode.S)) { verticalKey = -1; }
    //    else { verticalKey = 0; }

    //    if (horizontalKey != 0 && verticalKey != 0) { speedMod = 1 / Mathf.Sqrt(2); }
    //    else { speedMod = 1f; }
    //    if (Input.GetKey(KeyCode.LeftShift)) { speedMod *= 3; }//Shiftで高速移動

    //    transform.Translate(moveSpeed * speedMod * new Vector2(horizontalKey, verticalKey));//移動

        
    //}
    private void Update()
    {
        wheel += Input.mouseScrollDelta.y;
        if (wheel != 0)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= 2 * wheel, 5, 80);
            wheel = 0;
        }

        if (Input.GetKey(KeyCode.D)) { horizontalKey = 1; }//横移動の検出 
        else if (Input.GetKey(KeyCode.A)) { horizontalKey = -1; }
        else { horizontalKey = 0; }

        if (Input.GetKey(KeyCode.W)) { verticalKey = 1; }//縦移動の検出
        else if (Input.GetKey(KeyCode.S)) { verticalKey = -1; }
        else { verticalKey = 0; }

        if (horizontalKey != 0 && verticalKey != 0) { speedMod = 1 / Mathf.Sqrt(2); }
        else { speedMod = 1f; }
        if (Input.GetKey(KeyCode.LeftShift)) { speedMod *= 3; }//Shiftで高速移動

        transform.Translate(Time.unscaledDeltaTime * moveSpeed * speedMod * new Vector2(horizontalKey, verticalKey));//移動
    }
    public void MoveTo(Vector2 pos)
    {
        Vector3 newPos = new Vector3(pos.x, pos.y, -10);
        transform.position = newPos;
    }
}
