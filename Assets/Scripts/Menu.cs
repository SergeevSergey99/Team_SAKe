
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    private float tmp;
    public void Pause()
    {
        Time.timeScale = tmp - Time.timeScale;
    }

    // Use this for initialization
    void Start ()
    {

        tmp = Time.timeScale;
    }
    public void newGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void loadScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }
}