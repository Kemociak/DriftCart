using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSupermarket : MonoBehaviour

{
    [SerializeField]
    Canvas hudCanvas;
    [SerializeField]
    Canvas endCanvas;
    [SerializeField]
    TextMeshProUGUI finalTime;

    [SerializeField]
    TextMeshProUGUI timeText;

    [SerializeField]
    TextMeshProUGUI[] ingredients;

    [SerializeField]
    GameObject[] triggers;

    private float timer;

    void Start()
    {
        Time.timeScale = 1f;
        hudCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);


        timer = 0f;
        UpdateTimerUI();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Dodaje czas (w sekundach) od ostatniej klatki
        UpdateTimerUI();

        if (AreAllIngredientsInactive())
        {
            EndGame();
        }
    }
    private void UpdateTimerUI()
    {
        double sekundy = Math.Round(timer, 2);
        timeText.text = $"Time: {sekundy}s";
    }
    public void HandleCollectableCollision(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "HitBoxGreenPringles":
                ingredients[0].color = Color.green;

                break;
            case "HitBoxMozarella":
                ingredients[1].color = Color.green;

                break;
            case "HitBoxPumpkin":
                ingredients[2].color = Color.green;

                break;
            case "HitBoxMilk":
                ingredients[3].color = Color.green;

                break;
            case "HitBoxYoghurt":
                ingredients[4].color = Color.green;

                break;

        }
        other.gameObject.SetActive(false);
        Debug.Log("Kolizja!");
        Debug.Log($"Kolizja z obiektem: {other.gameObject.name}");

    }
    private bool AreAllIngredientsInactive()
    {
        foreach (var trigger in triggers)
        {
            if (trigger.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
    void EndGame()
    {
        Time.timeScale = 0f;
        hudCanvas.gameObject.SetActive(false);
        endCanvas.gameObject.SetActive(true);
        finalTime.text = $"Final time\n{timer}";
    }
}
