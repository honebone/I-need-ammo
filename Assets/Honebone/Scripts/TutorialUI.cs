using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Text tutorialText;
    PauseUI pauseUI;
    SceneLoadManager sceneLoadManager;
    [System.Serializable]
    public class Tutorial
    {
        public string key;

        [TextArea(3, 10)]
        public string tutorialText;

        public bool displayed;
    }
    [SerializeField]
    List<Tutorial> tutorialList = new List<Tutorial>();
    private void Start()
    {
        pauseUI=FindObjectOfType<PauseUI>();
        sceneLoadManager=FindObjectOfType<SceneLoadManager>();
    }
    public void DisplayTutorial(string key)
    {
        if (sceneLoadManager.enableTutorial)
        {
            foreach (Tutorial tutorial in tutorialList)
            {
                if (tutorial.key == key)
                {
                    if (!tutorial.displayed)
                    {
                        tutorial.displayed = true;
                        panel.SetActive(true);
                        tutorialText.text = tutorial.tutorialText;
                        pauseUI.SetTutorial(true);
                    }
                    return;
                }
            }
            print("ERROR!!!");
        }
    }
    public void Resume()
    {
        tutorialText.text = "";
        panel.SetActive(false);
        pauseUI.SetTutorial(false);
    }
}
