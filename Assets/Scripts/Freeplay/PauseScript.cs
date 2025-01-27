using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    Canvas pauseScreen;
    [SerializeField]
    Canvas HUD;
    [SerializeField]
    Button resumeButton;
    [SerializeField]
    Button exitButton;

    private bool isPaused = false;
    private void Start()
    {
        pauseScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
        HUD.gameObject.SetActive(false);
        Time.timeScale = 0f; // Pauza gry
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseScreen.gameObject.SetActive(false);
        HUD.gameObject.SetActive(true);
        Time.timeScale = 1f; // Wznowienie gry
        isPaused = false;
    }

    public void QuitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
