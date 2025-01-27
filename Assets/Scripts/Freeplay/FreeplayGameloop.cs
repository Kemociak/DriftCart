using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FreeplayGameloop : MonoBehaviour
{
    [SerializeField]
    int time;
    [SerializeField]
    int numberOfBags;
    [SerializeField]
    int numberOfPoints;
    [SerializeField]
    int timeReward;
    [SerializeField]
    int penalty;


    [SerializeField]
    Canvas hudCanvas;
    [SerializeField]
    Canvas endScreen;
 
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    TextMeshProUGUI finalScore;

    public GameObject[] activeSpawners;
    public List<GameObject> inactiveSpawners;

    int score;
  
                   //Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        hudCanvas.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        activeSpawners = GameObject.FindGameObjectsWithTag("Collectables");
        Debug.Log($"Znaleziono {activeSpawners.Length} worków");
        score = 0;
        UpdateScoreText();
        UpdateTimeText();
        foreach (GameObject s in activeSpawners)
        {
            s.gameObject.SetActive(false);
            inactiveSpawners.Add(s);
        }

        for (int i = 0; i < numberOfBags; i++)
        {
            ActivateRandomSpawner();
        }
        StartCoroutine(CountdownTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateRandomSpawner()
    {
        if (inactiveSpawners.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, inactiveSpawners.Count);

            inactiveSpawners[randomIndex].gameObject.SetActive(true);

            inactiveSpawners.RemoveAt(randomIndex);
        }
        else
        {
            Debug.LogWarning("Brak dostêpnych spawnerów do aktywacji!");
        }
    }

    public void HandleCollectableCollision(GameObject gameObject)
    {

        gameObject.SetActive(false);

        ActivateRandomSpawner();

        inactiveSpawners.Add(gameObject);

        score += numberOfPoints; 
        UpdateScoreText();
        time += timeReward;
        UpdateTimeText();
    }

    public void HandleOtherCollision()
    {
        score -= penalty;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }
    void UpdateTimeText()
    {
        timeText.text = $"Time: {time}s";
    }

    IEnumerator CountdownTimer()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            UpdateTimeText();
        }

        EndGame();
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Czas siê skoñczy³!");
        hudCanvas.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(true);
        finalScore.text = $"Final score {score}";

        Cursor.lockState = CursorLockMode.None;
    }
}
