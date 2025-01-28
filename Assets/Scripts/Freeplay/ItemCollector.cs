using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public FreeplayGameloop freeplayGameloop;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Collectables"))
        {
            freeplayGameloop.HandleCollectableCollision(collision.gameObject);
        }
        else if (!collision.CompareTag("Floor"))
        {
            freeplayGameloop.HandleOtherCollision();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Collectables") && !collision.collider.CompareTag("Floor"))
        {
            freeplayGameloop.HandleOtherCollision();
        }
    }
}
