using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCatcher2_ClawMachine : MonoBehaviour
{

    [Header("Score Settings")]
    public ParticleSystem coinExplosion;
    public Manager_ClawMovement2 managerClawMachine;

    public void OnTriggerEnter(Collider other)
    {
        // If we found a prize
        if (other.CompareTag("clearTarget"))
        {
            coinExplosion.Play();

            // Add the coins
            //managerClawMachine.playerCoins += other.GetComponent<Item_ClawMachine>().value;

            // Destroy the prize
            Destroy(other.gameObject);
            StageManager.onClear.Invoke();
        }
    }
}