using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Button restartButton;
    [SerializeField]
    Button exitButton;
    void Start()
    {
        restartButton.onClick.AddListener(RestartScene);
        exitButton.onClick.AddListener(ExitToMenu);
    }

    void RestartScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
    void ExitToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
