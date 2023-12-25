using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Text infoText;

    public void SetText(string text)
    {
        Vector3 offset=new Vector3 (0,0,-10);
        if(Input.mousePosition.x / Camera.main.pixelWidth < 0.5f) { offset.x = 0.17f* Camera.main.pixelWidth; }
        else { offset.x = -0.17f * Camera.main.pixelWidth; }
        panel.transform.position = Input.mousePosition + offset;
       //print(Input.mousePosition);
        //print(Camera.main.ScreenToWorldPoint(offset));
        panel.SetActive(true);
        infoText.text = text;
    }
    public void ResetText()
    {
        panel.transform.position = Input.mousePosition;
        panel.SetActive(false);
        infoText.text = "";
    }
}
