using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    GameObject UI;
    [SerializeField]
    Text tutorialText;
    [SerializeField]
    Color enabledColor;
    [SerializeField]
    Color disabledColor;
    [SerializeField]
    Toggle tutorialToggle;
    [SerializeField]
    Image blackout;
    bool enableTutorial;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (FindObjectOfType<SceneLoadManager>().enableTutorial)
        {
            tutorialToggle.isOn = true;
            enableTutorial = true;
            tutorialText.color = enabledColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayUI()
    {
        UI.SetActive(true);
    }
    public void ToggleTutorial()
    {
        enableTutorial = !enableTutorial;
        if (enableTutorial) { tutorialText.color = enabledColor; }
        else { tutorialText.color = disabledColor; }
    }
    public void StartGame()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        var wait = new WaitForSeconds(0.05f);
        Color c = Color.clear;
        blackout.color = c;
        for (int i = 0; i < 10; i++)
        {
            yield return wait;
            c.a += 0.1f;
            blackout.color = c;
        }
        yield return new WaitForSeconds(1f);
        FindObjectOfType<SceneLoadManager>().StartGame(enableTutorial);
    }
}
