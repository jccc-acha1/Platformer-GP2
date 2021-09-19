using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
    public GameObject Player;
    public int amount;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == Player) {
           UIHandler.pickupScore += amount;
           Destroy(gameObject);
        }
    } 

}
