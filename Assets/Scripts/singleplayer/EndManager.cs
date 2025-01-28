using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    [SerializeField]
    Button resetButton;

    [SerializeField]
    Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(ResetScene);
        exitButton.onClick.AddListener(ExitToMenu);
    }
    void ResetScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
    void ExitToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
