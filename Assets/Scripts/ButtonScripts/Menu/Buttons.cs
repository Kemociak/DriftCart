using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Singleplayer()
    {
        SceneManager.LoadScene("Singleplayer");
    }

    public void Freeplay()
    {
        SceneManager.LoadScene("Freeplay");
    }

    public void Race()
    {
        SceneManager.LoadScene("Race");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quitgame()
    {
        Application.Quit();
    }

}