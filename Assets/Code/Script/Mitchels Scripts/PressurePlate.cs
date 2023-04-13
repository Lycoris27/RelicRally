using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class PressurePlate : MonoBehaviour
    {
        public PressurePlateManager pressurePlateManager;
        public int id;

        private void OnTriggerEnter(Collider other)
        {
            // check if what hit was a player (by tag)
            if (other.gameObject.GetComponent<ronan.player.PlayerMovement>())
            {
                // if hit player, call pressurePlateManager.AddTriggeredPlate(id);
                pressurePlateManager.AddTriggeredPlate(id);
            }
        }
    }
}