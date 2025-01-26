using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public int itemCollected = 0;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("item"))
        {
            Debug.Log("podniesiono item!");

            itemCollected++;

            Destroy(collision.gameObject);
        }
    }
}
