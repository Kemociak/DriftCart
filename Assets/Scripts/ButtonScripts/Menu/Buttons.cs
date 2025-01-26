using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Singleplayer()
    {
        SceneManager.LoadScene("Map_Supermarket");
    }

    public void Freeplay()
    {
        SceneManager.LoadScene("Map_City");
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
