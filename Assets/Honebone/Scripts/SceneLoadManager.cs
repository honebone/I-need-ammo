using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public bool enableTutorial;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static SceneLoadManager instance;

    void Awake()
    {
        CheckInstance();
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(bool tutorial)
    {
        enableTutorial = tutorial;
        SceneManager.LoadScene("SampleScene");
    }
    public void EndGame()
    {
        SceneManager.LoadScene("Title");
    }
    
}
