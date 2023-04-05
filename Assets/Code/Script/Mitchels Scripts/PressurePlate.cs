using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public PressurePlateManager pressurePlateManager;
    public int id;

    private void OnTriggerEnter (Collider other)
    {
        // check if what hit was a player (by tag)
        if (other.tag == "Player")
        {
            // if hit player, call pressurePlateManager.AddTriggeredPlate(id);
            pressurePlateManager.AddTriggeredPlate(id);
        }
    }
}
