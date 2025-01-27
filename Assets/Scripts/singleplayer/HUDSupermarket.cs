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
    TextMeshProUGUI timeText;

    public TextMeshProUGUI[] itemTexts;
    private Dictionary<string, TextMeshProUGUI> shoppingList = new Dictionary<string, TextMeshProUGUI>();
    private float timer;

    void Start()
    {
        Time.timeScale = 1f;
        hudCanvas.gameObject.SetActive(true);

        foreach (TextMeshProUGUI text in itemTexts)
        {
            shoppingList.Add("HitBox" + text.text.Replace(" ", ""), text);
        }
        timer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timeText.text = "Time: " + Mathf.FloorToInt(timer).ToString() + "s";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible") && shoppingList.ContainsKey(other.gameObject.name))
        {
            shoppingList[other.gameObject.name].color = Color.green;
            
            Destroy(other.gameObject);
        }
    }
}
