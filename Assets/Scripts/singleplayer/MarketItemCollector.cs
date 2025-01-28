using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketItemCollector : MonoBehaviour
{

    [SerializeField]
    HUDSupermarket HUDSupermarket;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectables"))
        {
            HUDSupermarket.HandleCollectableCollision(other);
        }
    }

}
