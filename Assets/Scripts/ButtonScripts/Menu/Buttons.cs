using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    Canvas Menu;
    [SerializeField]
    Canvas SettingsScreen;
    private void Start()
    {
        Menu.gameObject.SetActive(true);
        SettingsScreen.gameObject.SetActive(false);
    }
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
        Menu.gameObject.SetActive(false);
        SettingsScreen.gameObject.SetActive(true);
    }
    public void Back()
    {
        Menu.gameObject.SetActive(true);
        SettingsScreen.gameObject.SetActive(false);
    }

    public void Quitgame()
    {
        Application.Quit();
    }

}
